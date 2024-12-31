<script>
import { library, icon } from '@fortawesome/fontawesome-svg-core';
import { faFilter, faFilterCircleXmark } from '@fortawesome/free-solid-svg-icons';

import { onMount } from 'svelte';
import cytoscape from 'cytoscape';
import cytoscapeDagre from 'cytoscape-dagre';
import cxtmenu from 'cytoscape-cxtmenu';


//Icons
import tableIcon from '../assets/table.png';
import measureIcon from '../assets/measure.png';
import calccolumnIcon from '../assets/calccolumn.png';

let cytoLineage;
const LineageStartingPositionX = 100;

class CytoscapeLineage{
    constructor(){
        this.cy = null;
        this.nodes = null;
        this.edges = null;
    }

    initLineage = async() => {
        const nodesURL = "http://localhost:8080/utilspbi/api/nodesdata";
        const edgesURL = "http://localhost:8080/utilspbi/api/edgesdata";

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
                this.nodes = nodes;
                this.edges = edges;
            });
    }

    filterNode = (filterText) => {
        this.clearFilter();
        var filteredNode = this.cy.nodes(`[id="${filterText}"]`);

        var incomingNodesForFilteredNode = filteredNode.predecessors();
        var outgoingNodesForFilteredNode = filteredNode.successors();

        var combinedNodes = incomingNodesForFilteredNode.union(outgoingNodesForFilteredNode).union(filteredNode);

        this.cy.elements().not(combinedNodes).hide();
    }

    clearFilter = () => {
        this.cy.nodes().show();
        this.cy.edges().show();
    }

    initCytoscape = () => {

    cytoscape.use(cytoscapeDagre);

    this.cy = cytoscape({
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
                    'width': 'mapData(nameLength, 1, 40, 250, 1300)',
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
            nodes: this.nodes,
            edges: this.edges
        }
    });

    //this.initCyContext();

    this.cy.on('tap', 'node', function (e) {
        var node = e.target; // Get the tapped node
        var OutgoingNodes = node.successors(); // Get edges connected to the node
        var IncomingNodes = node.predecessors(); // Get nodes connected by those edges

        this.cy.elements().addClass('faded'); // Dim all elements
        OutgoingNodes.removeClass('faded'); // Highlight the node and its connected nodes
        IncomingNodes.removeClass('faded');
        node.removeClass('faded');
    });

    this.cy.on('tap', function (e) {
        if (e.target === this.cy) {
            this.cy.elements().removeClass('faded');
        }
    });

    const rootNodes = this.cy.nodes().filter((node) => node.incomers('edge').length === 0);
    console.log(rootNodes);

    rootNodes.forEach((node, index) => {
        node.position({
            x: LineageStartingPositionX
        })
    });

    console.log("Cytoscape Finished");
    return this.cy;
    // document.getElementById("filterNodeButton").onclick = function () {
    //     filterNode(cy, "RowContextSumx");
    // };

    }

    initCyContext = () => {
        console.log("CyContext Init");
        cytoscape.use(cxtmenu);
        library.add(faFilter, faFilterCircleXmark);

        function createIconWithText(icon, text, textStyle = '', containerStyle = '') {
            return `
                <div style="display: flex; flex-direction: column; align-items: center; text-align: center; ${containerStyle}">
                    ${icon.html[0]}
                    <span style="font-size: 12px; color: #fff; ${textStyle}">${text}</span>
                </div>
            `;
        }

        const filterIcon = createIconWithText(icon(faFilter), 'Filter Node');
        const filterClearContent = createIconWithText(icon(faFilterCircleXmark), 'Clear Filter');

            let defaults = {
                menuRadius: function(ele){ return 50; }, // the outer radius (node center to the end of the menu) in pixels. It is added to the rendered size of the node. Can either be a number or function as in the example.
                selector: 'node', // elements mtching this Cytoscape.js selector will trigger cxtmenus
                commands: [ 
                { // example command
                    fillColor: 'rgba(78, 73, 73, 0.75)', // optional: custom background color for item
                    content: filterIcon, // html/text content to be displayed in the menu
                    contentStyle: {}, // css key:value pairs to set the command's css in js if you want
                    select: (ele) => { // a function to execute when the command is selected
                        this.filterNode( ele.id() ); // `ele` holds the reference to the active element
                    },
                    // hover: function(ele){ // a function to execute when the command is hovered
                    // console.log( ele.id() ) // `ele` holds the reference to the active element
                    // },
                    enabled: true // whether the command is selectable
                },
                { // example command
                    fillColor: 'rgba(78, 73, 73, 0.75)', // optional: custom background color for item
                    content: filterClearContent, // html/text content to be displayed in the menu
                    contentStyle: {}, // css key:value pairs to set the command's css in js if you want
                    select: (ele) => {
                        this.clearFilter();
                    },
                    enabled: true // whether the command is selectable
                }
                ], // function( ele ){ return [ /*...*/ ] }, // a function that returns commands or a promise of commands
                fillColor: 'rgba(0, 0, 0, 0.75)', // the background colour of the menu
                activeFillColor: 'rgba(1, 105, 217, 0.75)', // the colour used to indicate the selected command
                activePadding: 20, // additional size in pixels for the active command
                indicatorSize: 24, // the size in pixels of the pointer to the active command, will default to the node size if the node size is smaller than the indicator size, 
                separatorWidth: 3, // the empty spacing in pixels between successive commands
                spotlightPadding: 4, // extra spacing in pixels between the element and the spotlight
                adaptativeNodeSpotlightRadius: false, // specify whether the spotlight radius should adapt to the node size
                minSpotlightRadius: 24, // the minimum radius in pixels of the spotlight (ignored for the node if adaptativeNodeSpotlightRadius is enabled but still used for the edge & background)
                maxSpotlightRadius: 38, // the maximum radius in pixels of the spotlight (ignored for the node if adaptativeNodeSpotlightRadius is enabled but still used for the edge & background)
                openMenuEvents: 'cxttapstart taphold', // space-separated cytoscape events that will open the menu; only `cxttapstart` and/or `taphold` work here
                itemColor: 'white', // the colour of text in the command's content
                itemTextShadowColor: 'transparent', // the text shadow colour of the command's content
                zIndex: 9999, // the z-index of the ui div
                atMouse: false, // draw menu at mouse position
                outsideMenuCancel: 1 // if set to a number, this will cancel the command if the pointer is released outside of the spotlight, padded by the number given 
            };

            this.menu = this.cy.cxtmenu( defaults );
    }
}

onMount(() => {
        cytoLineage = new CytoscapeLineage();
        cytoLineage.initLineage().then(() => {
            cytoLineage.initCytoscape();
            cytoLineage.initCyContext();
        });  
    });

export function filterCytoscapeNode(filterText){
    cytoLineage.filterNode(filterText);
}

</script>
<div id="cy-container">

</div>
<style>
#cy-container {
    height: 100%;
}
</style>