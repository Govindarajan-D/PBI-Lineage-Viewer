<script lang="ts">
  import dagre from '@dagrejs/dagre';
  import {
    SvelteFlow,
    Background,
    Position,
    ConnectionLineType,
    Panel,
    type Node,
    type Edge,
  } from '@xyflow/svelte';

  import '@xyflow/svelte/dist/style.css';

  import CalcNode from './CalcNode.svelte';
  import { onMount } from 'svelte';

  const nodeTypes = {
    selectorNode: CalcNode,
  };
 
  let nodes = [];
  let edges = []; 
  const dagreGraph = new dagre.graphlib.Graph();
  dagreGraph.setDefaultEdgeLabel(() => ({}));

  const nodeWidth = 172;
  const nodeHeight = 36;

function getLayoutedElements(nodes: Node[], edges: Edge[], direction = 'TB') {
    const isHorizontal = direction === 'LR';
    dagreGraph.setGraph({ rankdir: direction });

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
      // so it matches the React Flow node anchor point (top left).
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

const position = { x: 0, y: 0 };
const edgeType = 'smoothstep';

const baseURL = "http://localhost:8080/utilspbi/api/";

var svelte_nodes, svelte_edges;

async function fetchdata() {
  const nodesURL = baseURL + "sveltenodes";
  const edgesURL = baseURL + "svelteedges";

  return Promise.all([
      fetch(nodesURL).then(response => response.json()),
      fetch(edgesURL).then(response => response.json())
  ])
      .then(([nodes, edges]) => {
          nodes.forEach(element => {
              if (element.data && element.data.name) {
                  element.data.nameLength = element.data.name.length;
              }
          });
          svelte_nodes = nodes;
          svelte_edges = edges;
      });
}

onMount(() => {
  fetchdata().then(() => {
    //nodes = initialNodes;
    //edges = initialEdges;
    const layoutedElements = getLayoutedElements(svelte_nodes, svelte_edges, 'LR');
    nodes = layoutedElements.nodes;
    edges = layoutedElements.edges;
    // console.log(nodes);
  })
});



function onLayout(direction: string) {
  const layoutedElements = getLayoutedElements(svelte_nodes, svelte_edges, direction);

  nodes = layoutedElements.nodes;
  edges = layoutedElements.edges;
}
</script>

<SvelteFlow
  bind:nodes
  bind:edges
  {nodeTypes}
  fitView
  connectionLineType={ConnectionLineType.SmoothStep}
  defaultEdgeOptions={{ type: 'bezier', animated: false }}

>
  <Panel position="top-right">
    <button onclick={() => onLayout('TB')}>vertical layout</button>
    <button onclick={() => onLayout('LR')}>horizontal layout</button>
  </Panel>
  <Background />
</SvelteFlow>
