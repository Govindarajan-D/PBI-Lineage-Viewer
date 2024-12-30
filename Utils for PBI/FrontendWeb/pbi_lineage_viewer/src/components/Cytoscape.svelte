<script>
import {onMount} from 'svelte';
import cytoscape from 'cytoscape';
import cytoscapeDagre from 'cytoscape-dagre';
import {initCyContext} from '../CytoscapeContext';
import cxtmenu from 'cytoscape-cxtmenu';


//Icons
import tableIcon from '../assets/table.png';
import measureIcon from '../assets/measure.png';
import calccolumnIcon from '../assets/calccolumn.png';

const LineageStartingPositionX = 100;

onMount(() => {
        initLineage();
    });


function initLineage() {
    const nodesURL = "http://localhost:8080/utilspbi/api/nodesdata";
    const edgesURL = "http://localhost:8080/utilspbi/api/edgesdata";

    Promise.all([
        fetch(nodesURL).then(response => response.json()),
        fetch(edgesURL).then(response => response.json())
    ])
        .then(([nodes, edges]) => {
            nodes.forEach(element => {
                if (element.data && element.data.name) {
                    element.data.nameLength = element.data.name.length;
                }
            });
            initCytoscape(nodes, edges)
        })
}

function filterNode(cy, filterText) {
    var filteredNode = cy.nodes(`[id="${filterText}"]`);

    console.log(filteredNode);

    var incomingNodesForFilteredNode = filteredNode.predecessors();
    var outgoingNodesForFilteredNode = filteredNode.successors();

    var combinedNodes = incomingNodesForFilteredNode.union(outgoingNodesForFilteredNode);

    cy.elements().not(combinedNodes).hide();
}

function initCytoscape(nodes, edges) {

    cytoscape.use(cytoscapeDagre);
    cytoscape.use(cxtmenu);

    var cy = cytoscape({
        container: document.querySelector('#cy-container'),

        layout: {
            name: 'dagre',
            rankDir: 'LR',
            rankSep: 400,
            edgeSep: 30,
            nodeSep: 50
        },

        style: [
            {
                selector: 'node',
                style: {
                    'background-image': function(ele){
                        const type = ele.data('objectType');
                        if(type === "CALC_COLUMN"){
                            return `url(${calccolumnIcon})`;
                        }
                        else if (type === "MEASURE"){
                           return `url(${measureIcon})`;
                        }
                        else {
                            return `url(${tableIcon})`;
                        }
                    },
                    'background-fit': 'contain', // Ensure the icon fits within the node
                    'background-position-x': '10%', // Move the icon to the left
                    'background-position-y': '50%', // Center it vertically
                    'background-size': '1px', // Set icon size
                    'shape': 'data(faveShape)',
                    'width': 'mapData(nameLength, 1, 40, 200, 1200)',
                    'height': '70',
                    'content': 'data(name)',
                    'text-valign': 'center',
                    'text-outline-width': 2,
                    'text-outline-color': 'data(faveColor)',
                    'background-color': 'data(faveColor)',
                    'color': '#fff',
                    'font-size': '40px'
                }
            },
            {
                selector: 'edge',
                style: {
                    'curve-style': 'unbundled-bezier',
                    'opacity': 0.666,
                    'width': 'mapData(strength, 70, 100, 2, 6)',
                    'target-arrow-shape': 'triangle',
                    'line-color': 'data(faveColor)',
                    'source-arrow-color': 'data(faveColor)',
                    'target-arrow-color': 'data(faveColor)',
                    'arrow-scale': 1.5
                }
            },
            {
                selector: ':selected',
                style: {
                    'curve-style': 'bezier',
                    'opacity': 1,
                    'width': 'mapData(strength, 70, 100, 2, 6)',
                    'target-arrow-shape': 'triangle',
                    'source-arrow-shape': 'circle',
                    'line-color': 'black',
                    'source-arrow-color': 'data(faveColor)',
                    'target-arrow-color': 'data(faveColor)'
                }
            },
            {
                selector: 'edge.questionable',
                style: {
                    'line-style': 'dotted',
                    'target-arrow-shape': 'diamond'
                }
            },
            {
                selector: '.faded',
                style: {
                    'opacity': 0.25
                }
            }
        ],
        elements: {
            nodes: nodes,
            edges: edges
        }
    });

    initCyContext(cy);

    cy.on('tap', 'node', function (e) {
        var node = e.target; // Get the tapped node
        var OutgoingNodes = node.successors(); // Get edges connected to the node
        var IncomingNodes = node.predecessors(); // Get nodes connected by those edges

        cy.elements().addClass('faded'); // Dim all elements
        OutgoingNodes.removeClass('faded'); // Highlight the node and its connected nodes
        IncomingNodes.removeClass('faded');
        node.removeClass('faded');
    });

    cy.on('tap', function (e) {
        if (e.target === cy) {
            cy.elements().removeClass('faded');
        }
    });

    const rootNodes = cy.nodes().filter((node) => node.incomers('edge').length === 0);
    console.log(rootNodes);

    rootNodes.forEach((node, index) => {
        node.position({
            x: LineageStartingPositionX
        })
    });

    // document.getElementById("filterNodeButton").onclick = function () {
    //     filterNode(cy, "RowContextSumx");
    // };

}
</script>
<div id="cy-container">

</div>
<style>
#cy-container {
    height: 100%;
}
</style>