<script lang="ts">
  import { Handle, Position, type NodeProps } from "@xyflow/svelte";
  let { isConnectable, id, data }: NodeProps = $props();
  // You can optionally pass color and details through `data`
  const bgColor = "#f0f8ff";
  const detail = "No extra info";
</script>

<div class="custom-node" style="--bg-color: {bgColor}">
  <Handle type="target" position="left" />
  <div class="node-container {data.CalcType}">
    <span class="node-icon"></span>
    <span class="node-name"><b>{data.CalcName}</b></span>
  </div>
  <Handle type="source" position="right" />
</div>

<style>
  .custom-node {
    border-radius: 8px;
    border: 2px solid #ccc;
    width: 180px;
    background-color: var(--bg-color);
    box-shadow: 2px 2px 6px rgba(0, 0, 0, 0.1);
    font-family: sans-serif;
    overflow: hidden;
  }

  .title {
    padding: 10px;
    font-weight: bold;
    background-color: rgba(0, 0, 0, 0.05);
    border-bottom: 1px solid #ccc;
  }

  .detail {
    font-size: 12px;
    color: #555;
    padding: 6px 10px;
    background-color: rgba(0, 0, 0, 0.02);
  }

  .Table .node-icon::before {
    content: "📄";
  }
  .node-container {
    display: flex;
    align-items: center;
    justify-content: space-between;
    border: 1px solid #ccc;
    background-color: #fefefe;
    padding: 8px 12px;
    border-radius: 6px;
    font-family: sans-serif;
    font-size: 14px;
    width: 160px;
    box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
  }

  .node-name {
    font-weight: 500;
    color: #333;
    justify-content: left;
    align-items: left;
  }

  .node-icon {
    
    font-size: 23px;
  }
</style>
