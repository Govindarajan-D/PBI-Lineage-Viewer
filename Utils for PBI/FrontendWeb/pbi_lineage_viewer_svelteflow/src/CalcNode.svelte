<script lang="ts">
    import { Handle, Position, type NodeProps } from '@xyflow/svelte';
    let { isConnectable, id, data }: NodeProps = $props();
      // You can optionally pass color and details through `data`
  const bgColor = '#f0f8ff';
  const detail = 'No extra info';

  </script>

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
  </style>
  

<div class="custom-node" style="--bg-color: {bgColor}">
    <Handle type="target" position="left" />

    <div class="title">{data.CalcName}</div>

    <div class="detail">{data.CalcType}</div>

    <Handle type="source" position="right" />
</div>