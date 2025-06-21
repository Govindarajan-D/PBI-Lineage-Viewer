<script lang="ts">
  import dagre from "@dagrejs/dagre";
  import {
    SvelteFlow,
    Background,
    Position,
    ConnectionLineType,
    Panel,
    useSvelteFlow,
    type Node,
    type Edge,
    type NodeEventWithPointer,
  } from "@xyflow/svelte";

  import { writable } from "svelte/store";
  import { onMount } from "svelte";
  import CalcNode from "./CalcNode.svelte";
  import ContextMenu from "./ContextMenu.svelte";
  import "@xyflow/svelte/dist/style.css";
  import { getAncestors, getDescendants } from "./utility";

  const nodeTypes = {
    selectorNode: CalcNode,
  };

  let flowRef;
  const nodes = writable([]);
  const edges = writable([]);
  const nodeWidth = 172;
  const nodeHeight = 36;

  const {fitView} = useSvelteFlow();

  const baseURL = "http://localhost:8080/utilspbi/api/";
  var svelte_nodes, svelte_edges;

  // Handling context menu
  let menu: {
    id: string;
    top?: number;
    left?: number;
    right?: number;
    bottom?: number;
  } | null = $state(null);
  let clientWidth: number = $state();
  let clientHeight: number = $state();

  // This function is used to layout the nodes and edges using the dagre library.
  // It sets the graph direction, adds nodes and edges to the graph, and then computes the layout.
  function getLayoutedElements(nodes: Node[], edges: Edge[], direction = "TB") {
    //Define constants for the graph layout
    const rankSep = 250; // Space between ranks - Horizontally
    const nodeSep = 50;

    const dagreGraph = new dagre.graphlib.Graph();
    dagreGraph.setDefaultEdgeLabel(() => ({}));
    
    const isHorizontal = direction === "LR";
    dagreGraph.setGraph({ rankdir: direction, ranksep: rankSep, nodesep: nodeSep});

    nodes.forEach((node) => {
      dagreGraph.setNode(node.id, { width: nodeWidth, height: nodeHeight });
    });

    edges.forEach((edge) => {
      dagreGraph.setEdge(edge.source, edge.target);
    });

    dagre.layout(dagreGraph);

    const layoutedNodes = nodes.map((node) => {
      const nodeWithPosition = dagreGraph.node(node.id);
      node.targetPosition = isHorizontal ? Position.Left : Position.Top;
      node.sourcePosition = isHorizontal ? Position.Right : Position.Bottom;

      // We are shifting the dagre node position (anchor=center center) to the top left
      // so it matches the Svelte Flow node anchor point (top left).
      return {
        ...node,
        position: {
          x: nodeWithPosition.x - nodeWidth / 2,
          y: nodeWithPosition.y - nodeHeight / 2,
        },
      };
    });
    

    return { nodes: layoutedNodes, edges };
  }

  // Fetches nodes and edges data from the API and processes it.
  // It is asynchronous and returns a promise that resolves when the data is fetched.
  async function fetchdata() {
    const nodesURL = baseURL + "sveltenodes";
    const edgesURL = baseURL + "svelteedges";

    return Promise.all([
      fetch(nodesURL).then((response) => response.json()),
      fetch(edgesURL).then((response) => response.json()),
    ]).then(([nodes, edges]) => {
      nodes.forEach((element) => {
        if (element.data && element.data.name) {
          element.data.nameLength = element.data.name.length;
        }
      });
      svelte_nodes = nodes;
      svelte_edges = edges;
    });
  }

  // The async function fetchdata is called when the component is mounted.
  onMount(() => {
    fetchdata().then(() => {
      const layoutedElements = getLayoutedElements(
        svelte_nodes,
        svelte_edges,
        "LR",
      );
      nodes.set(layoutedElements.nodes);
      edges.set(layoutedElements.edges);
    });
  });

  function onLayout(direction: string) {
    const layoutedElements = getLayoutedElements(
      svelte_nodes,
      svelte_edges,
      direction,
    );

    nodes.set(layoutedElements.nodes);
    edges.set(layoutedElements.edges);
  }

  function filterNode(filter_id) {

    const unioned_nodes = Array.from(new Set([...getAncestors(svelte_edges, filter_id),...getDescendants(svelte_edges, filter_id), filter_id]));
    const filtered_nodes = svelte_nodes.filter((node) => unioned_nodes.includes(node.id));
    const filtered_edges = svelte_edges.filter((edge) => unioned_nodes.includes(edge.source) || unioned_nodes.includes(edge.target));
    const layoutedElements = getLayoutedElements(
      filtered_nodes,
      filtered_edges,
      "LR",
    );

    console.log("Layouted Nodes:", layoutedElements);
    
    nodes.set(layoutedElements.nodes);
    edges.set(layoutedElements.edges);

    const fitViewOptions = {
      padding: 0.5,
      duration: 800, // Animate the transition over 800ms
      maxZoom: 2
    };

    setTimeout(() => {
      fitView(fitViewOptions);
    }, 0);
  }



  const handleContextMenu: NodeEventWithPointer = ({ event, node }) => {
    // Prevent native context menu from showing
    event.preventDefault();

    // Calculate position of the context menu. We want to make sure it
    // doesn't get positioned off-screen.
    menu = {
      id: node.id,
      top: event.clientY < clientHeight - 200 ? event.clientY : undefined,
      left: event.clientX < clientWidth - 200 ? event.clientX : undefined,
      right:
        event.clientX >= clientWidth - 200
          ? clientWidth - event.clientX
          : undefined,
      bottom:
        event.clientY >= clientHeight - 200
          ? clientHeight - event.clientY
          : undefined,
    };
  };

  // Close the context menu if it's open whenever the window is clicked.
  function handlePaneClick() {
    menu = null;
  }
</script>

<div style="height:100vh;" bind:clientWidth bind:clientHeight>
  <SvelteFlow
    bind:this={flowRef}
    bind:nodes={$nodes}
    bind:edges={$edges}
    {nodeTypes}
    fitView
    onnodecontextmenu={handleContextMenu}
    onpaneclick={handlePaneClick}
    connectionLineType={ConnectionLineType.SmoothStep}
    defaultEdgeOptions={{ type: "bezier", animated: false }}
  >
    <Panel position="top-right">
      <button onclick={() => onLayout("TB")}>vertical layout</button>
      <button onclick={() => onLayout("LR")}>horizontal layout</button>
      <button onclick={filterNode}>Filter Nodes</button>
    </Panel>

    <Background />
    <!-- Context Menu (Right-Click menu) -->
    {#if menu}
      <ContextMenu
        onclick={handlePaneClick}
        filterNode={(id) => filterNode(id)}
        id={menu.id}
        top={menu.top}
        left={menu.left}
        right={menu.right}
        bottom={menu.bottom}
      />
    {/if}
  </SvelteFlow>
</div>
