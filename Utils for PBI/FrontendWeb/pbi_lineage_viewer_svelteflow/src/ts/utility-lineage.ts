
// Breadth-First Search (BFS) to find all predecessors/successors of a node in a directed graph
export function bfs(edges, startNode, direction = 'incomers' ){
    const queue = [startNode];
    const visited = new Set();
    const hierNodes = new Set();

    // We create edgeFilter function to filter according to the direction
    const edgeFilter = direction === "incomers"
        ? (edge, node) => edge.target === node
        : (edge, node) => edge.source === node;

    while (queue.length > 0) {
        // shift does something similar to pop but on the first element
        // if we had used pop, it would have been dfs ;)
        const currentNode = queue.shift();
        // Let's take a -> b -> d, e
        // Let's walk back from `e`. The source of `e` will be `b`. 
        // The startNode will be [e]. If the direction is incomers, we check where `e` is in target
        // and then we filter to those nodes. Then for each of those neighboring nodes
        // we loop through to find the next predecessor. Here for `e`, the source is `b`.
        // We push b to the queue so that we can repeat the same steps. 
        // We also push b to the hier Nodes. 

        if (!visited.has(currentNode)){
            visited.add(currentNode);
            const neighbors = edges.filter(edge => edgeFilter(edge, currentNode));

            for (const neighbor of neighbors){
                const nextNode = direction === "incomers"? neighbor.source : neighbor.target;
                if(!visited.has(nextNode)){
                    queue.push(nextNode);
                    hierNodes.add(nextNode);
                }
            }

        }
    }

    return hierNodes;
}

// Call BFS to find ancestors
export function getAncestors(edges, startNode){
    return bfs(edges, startNode, "incomers");
}

// Call BFS to find descendants
export function getDescendants(edges, startNode){
    return bfs(edges, startNode, "outgoers");
}