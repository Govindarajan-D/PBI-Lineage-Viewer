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
  

//  import DownloadLineage from './DownloadLineage.svelte';

  import { writable } from "svelte/store";
  import { onMount } from "svelte";
  import DisplayNode from "./flowcomponents/DisplayNode.svelte";
  import ContextMenu from "./ContextMenu.svelte";
  import ModalBox from "./ModalBox.svelte";
  import "@xyflow/svelte/dist/style.css";
  import { getAncestors, getDescendants } from "../ts/utility";

  const nodeTypes = {
    selectorNode: DisplayNode,
  };

  let flowRef;

  const nodes = writable([]);
  const edges = writable([]);
  const nodeWidth = 172;
  const nodeHeight = 36;

  const {fitView, setZoom, getViewport} = useSvelteFlow();

  const baseURL = "http://localhost:8080/utilspbi/api/";
  var svelteNodes, svelteEdges;

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
  let modalData = $state(null);

  // This function is used to layout the nodes and edges using the dagre library.
  // It sets the graph direction, adds nodes and edges to the graph, and then computes the layout.
  const getLayoutedElements = (nodes: Node[], edges: Edge[], direction = "TB") => {
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
  async function retrieveData() {
    const nodesURL = baseURL + "nodes";
    const edgesURL = baseURL + "edges";

    return Promise.all([
      fetch(nodesURL).then((response) => response.json()),
      fetch(edgesURL).then((response) => response.json()),
    ]).then(([nodes, edges]) => {
      nodes.forEach((element) => {
        if (element.data && element.data.name) {
          element.data.nameLength = element.data.name.length;
        }
      });
      svelteNodes = nodes;
      svelteEdges = edges;
    });
  }

  const setNodesAndEdges = (layoutedElements) => {
    nodes.set(layoutedElements.nodes);
    edges.set(layoutedElements.edges);

    const fitViewOptions = {
      padding: 0.5,
      duration: 800,
      maxZoom: 2
    };

    setTimeout(() => {
      fitView(fitViewOptions);
    }, 0);
  }

  // The async function fetchdata is called when the component is mounted.
  onMount(() => {
    retrieveData().then(() => {
      svelteNodes = svelteNodes.map((node) => ({
        ...node,
        data: {
          ...node.data,
          onShowBox: handleShowBox,
          expanded: false,
          showBox: false
        }
      }));

      const layoutedElements = getLayoutedElements(
        svelteNodes,
        svelteEdges,
        "LR",
      );

      setNodesAndEdges(layoutedElements);
    });
  });

  const onLayout = (direction: string) => {
    const layoutedElements = getLayoutedElements(
      svelteNodes,
      svelteEdges,
      direction,
    );

    setNodesAndEdges(layoutedElements);
  }

  const filterNode = (filter_id) => {

    const unioned_nodes = Array.from(new Set([...getAncestors(svelteEdges, filter_id),...getDescendants(svelteEdges, filter_id), filter_id]));
    const filtered_nodes = svelteNodes.filter((node) => unioned_nodes.includes(node.id));
    const filtered_edges = svelteEdges.filter((edge) => unioned_nodes.includes(edge.source) || unioned_nodes.includes(edge.target));
    const layoutedElements = getLayoutedElements(
      filtered_nodes,
      filtered_edges,
      "LR",
    );
    
    setNodesAndEdges(layoutedElements);
  }

  const clearFilter = () => {
    const layoutedElements = getLayoutedElements(
      svelteNodes,
      svelteEdges,
      "LR",
    );

    setNodesAndEdges(layoutedElements);
  }
  // To handle clicking of a node, which highlights the complete lineage (front and back of node)
  // TO-FIX: Non-related edges get highlighted and a methodology for removing highlight should be coded.
  const handleNodeClick = (event) => {
    const clickedNode = event.node.id;
    const unioned_nodes = Array.from(new Set([...getAncestors(svelteEdges, clickedNode),...getDescendants(svelteEdges, clickedNode), clickedNode]));
    const highlighted_nodes = svelteNodes.map((node) => ({
      ...node,
      data: {
          ...node.data,
          highlighted: unioned_nodes.includes(node.id),
        }
    }));

    const highlighted_edges = svelteEdges.map((edge) => ({
      ...edge,
      style: unioned_nodes.includes(edge.source) || unioned_nodes.includes(edge.target)
      ? "stroke: orange; stroke-width: 3;"
      : "",
    }));

    const layoutedElements = getLayoutedElements(
      highlighted_nodes,
      highlighted_edges,
      "LR",
    );

    setNodesAndEdges(layoutedElements);
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

  const zoom = (InOrOut) => {
    const currentZoom = getViewport().zoom || 1;
    InOrOut == "In"? setZoom(currentZoom * 1.1) : setZoom(currentZoom / 1.1);
  }

  // Close the context menu if it's open whenever the window is clicked.
  const handlePaneClick = () => {
    menu = null;
  }
  
  const handleShowBox = (modalBoxData) => {
    modalData = modalBoxData;
  }

  const closeShowBox = () => {
    modalData = null;
  }

</script>

<div style="height:100vh;" bind:clientWidth bind:clientHeight>
  <SvelteFlow
    bind:this={flowRef}
    bind:nodes={$nodes}
    bind:edges={$edges}
    {nodeTypes}
    fitView
    onnodeclick={handleNodeClick}
    onnodecontextmenu={handleContextMenu}
    onpaneclick={handlePaneClick}
    connectionLineType={ConnectionLineType.SmoothStep}
  >
    <Panel position="top-right">
      <button class="button-mui" onclick={fitView}>Fit View</button>
      <button class="button-mui" onclick={() => zoom("In")}>Zoom In</button>
      <button class="button-mui" onclick={() => zoom("Out")}>Zoom Out</button>
    </Panel>
    <!--<DownloadLineage/>-->
    <Background />
    <!-- Context Menu (Right-Click menu) -->
    {#if menu}
      <ContextMenu
        onclick={handlePaneClick}
        filterNode={(id) => filterNode(id)}
        clearFilter={() => clearFilter()}
        id={menu.id}
        top={menu.top}
        left={menu.left}
        right={menu.right}
        bottom={menu.bottom}
      />
    {/if}
  </SvelteFlow>
  {#if modalData}
    <ModalBox modalData={modalData} closeModal={closeShowBox}/>
  {/if}
</div>
<style>
  .button-mui {
  background: #4f7faf;
  color: #fff;
  border: none;
  border-radius: 4px;
  padding: 0.5em 1.2em;
  font-size: 0.85em;
  font-family: inherit;
  cursor: pointer;
  margin: 0 0.25em;
  box-shadow: 0 2px 6px #1976d222;
  transition: background 0.15s, box-shadow 0.15s;
}
.button-mui:hover,
.button-mui:focus {
  background: #1565c0;
  box-shadow: 0 4px 12px #1976d244;
  outline: none;
}
</style>