<script lang="ts"> 
  let {
    id,
    top,
    left,
    right,
    bottom,
    onclick,
    filterNode,
    clearFilter
  }: {
    id: string;
    top: number | undefined;
    left: number | undefined;
    right: number | undefined;
    bottom: number | undefined;
    onclick: () => void;
    filterNode: (id: string) => void;
    clearFilter: () => void;
  } = $props();

</script>
 
<div
  style="top: {top}px; left: {left}px; right: {right}px; bottom: {bottom}px;"
  class="context-menu"
  {onclick}
>
  <p style="margin: 0.5em;">
    <small>node: {id}</small>
  </p>
  <div class="context-menu-divider"></div>
  <button onclick={() => filterNode(id)}>Filter Node</button>
  <button onclick={clearFilter}>Clear Filter</button>
</div>
 
<style>
  .context-menu {
    background: #fff;
    border: 1px solid #e0e0e0;
    border-radius: 10px;
    box-shadow: 0 8px 32px 0 rgba(31, 38, 135, 0.15);
    position: absolute;
    z-index: 10;
    min-width: 170px;
    font-family: 'Segoe UI', Arial, sans-serif;
    padding: 0.5em 0;
    animation: fadeIn 0.15s;
  }

  .context-menu p {
    margin: 0.5em 1em 0.5em 1em;
    color: #020da3;
    font-size: 0.95em;
    letter-spacing: 0.02em;
  }

  .context-menu-divider {
    border-bottom: 1px solid #eee;
    margin: 0.3em 0;
  }

  .context-menu button {
    border: none;
    background: none;
    display: block;
    padding: 0.7em 1.2em;
    text-align: left;
    width: 100%;
    font-size: 1em;
    color: #333;
    cursor: pointer;
    transition: background 0.15s, color 0.15s;
    border-radius: 6px;
  }

  .context-menu button:hover,
  .context-menu button:focus {
    background: #f5f7fa;
    color: #1976d2;
    outline: none;
  }

  .context-menu button:active {
    background: #e3eafc;
    color: #0d47a1;
  }

  @keyframes fadeIn {
    from { opacity: 0; transform: translateY(8px);}
    to { opacity: 1; transform: translateY(0);}
  }
</style>