<script lang="ts">
  import { useEdges, useNodes, useSvelteFlow } from '@xyflow/svelte';
 
  let {
    id,
    top,
    left,
    right,
    bottom,
    onclick,
  }: {
    id: string;
    top: number | undefined;
    left: number | undefined;
    right: number | undefined;
    bottom: number | undefined;
    onclick: () => void;
  } = $props();
 
  const { deleteElements } = useSvelteFlow();
 
  const nodes = useNodes();
 
  function filterNode() {
    console.log(`Filtering node: ${id}`);
  }
 
  function clearFilter() {
  }
</script>
 
<div
  style="top: {top}px; left: {left}px; right: {right}px; bottom: {bottom}px;"
  class="context-menu"
  {onclick}
>
  <p style="margin: 0.5em;">
    <small>node: {id}</small>
  </p>
  <button onclick={filterNode}>Filter Node</button>
  <button onclick={clearFilter}>Clear Filter</button>
</div>
 
<style>
  .context-menu {
    background: white;
    border-style: solid;
    box-shadow: 10px 19px 20px rgba(0, 0, 0, 10%);
    position: absolute;
    z-index: 10;
  }
 
  .context-menu button {
    border: none;
    display: block;
    padding: 0.5em;
    text-align: left;
    width: 100%;
  }
 
  .context-menu button:hover {
    background: white;
  }
</style>
