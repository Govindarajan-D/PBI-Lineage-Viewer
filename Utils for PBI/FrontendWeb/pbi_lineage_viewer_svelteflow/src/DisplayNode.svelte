<script lang="ts">
  import { Handle, Position, type NodeProps } from "@xyflow/svelte";
  let { isConnectable, id, data, highlighted = false }: NodeProps & { highlighted?: boolean } = $props();
  // You can optionally pass color and details through `data`
  const bgColor = "#f0f8ff";

  import TableIcon from './assets/Table.svg?url';
  import MeasureIcon from './assets/Measure.svg?url';
  import ColumnIcon from './assets/Column.svg?url';
  import CalcColumnIcon from './assets/Calc Column.svg?url';
  import CalcTableIcon from './assets/Calc Table.svg?url';


  const iconMap = {
    Table: TableIcon,
    Measure: MeasureIcon,
    Column: ColumnIcon,
    'Calc Column': CalcColumnIcon,
    'Calc Table': CalcTableIcon
    // Add more as needed
  };

</script>

<div class="mui-node {highlighted ? 'highlighted': ''} {data.CalcType}" style="--bg-color: {bgColor}">
  <Handle type="target" position="left" />
  <div class="mui-node-content">
    <span class="mui-node-icon">
      <img src= {iconMap[data.CalcType]} alt="icon" class="icon-img"/>
    </span>
    <span class="mui-node-name"><b>{data.CalcName}</b></span>
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

  .custom-node.highlighted {
    border: 3px solid orange;
    box-shadow: 0 0 10px 2px #ffa50055;
    background-color: #fffbe6;
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
  .mui-node {
    display: flex;
    align-items: center;
    border-radius: 0px;
    border: none;
    box-shadow: 0 2px 8px rgba(60, 72, 100, 0.10), 0 1.5px 4px rgba(60, 72, 100, 0.08);
    background: var(--bg-color);
    min-width: 200px;
    min-height: 58px;
    font-family: 'Roboto', 'Segoe UI', Arial, sans-serif;
    position: relative;
    transition: box-shadow 0.2s, border 0.2s, background 0.2s;
    border-left: 6px solid #1976d2;
    overflow: visible;
  }
  
  .mui-node.Table { border-left-color: #43a047; }
  .mui-node.Measure { border-left-color: #fbc02d; }
  .mui-node.highlighted {
    box-shadow: 0 0 16px 4px #ffa50055, 0 2px 8px rgba(60, 72, 100, 0.13);
    border-left: 8px solid #ff9800;
    background: #fffde7;
  }
  .mui-node:hover {
    box-shadow: 0 4px 16px 2px #1976d233, 0 2px 8px rgba(60, 72, 100, 0.13);
    background: #f5f7fa;
  }
  .mui-node-content {
    display: flex;
    align-items: center;
    gap: 14px;
    padding: 10px 18px;
    flex: 1;
    min-width: 0;
  }
  .mui-node-icon {
    font-size: 26px;
    color: #1976d2;
    display: flex;
    align-items: center;
    justify-content: center;
    min-width: 28px;
  }
  .mui-node.Table .mui-node-icon { color: #43a047; }
  .mui-node.Measure .mui-node-icon { color: #fbc02d; }
  .mui-node.highlighted .mui-node-icon { color: #ff9800; }
  .mui-node-name {
    font-weight: normal;
    color: #222;
    font-size: 1.8em;
    letter-spacing: 0.01em;
    white-space: nowrap;
    overflow: hidden;
    text-overflow: ellipsis;
    flex: 1;
  }
  .mui-node-icon {
    display: flex;
    align-items: center;
    justify-content: center;
    min-width: 28px;
    min-height: 28px;
  }
  .icon-img {
    width: 30px;
    height: 30px;
    object-fit: contain;
    border-radius: 4px; /* optional, for rounded icons */
    box-shadow: 0 1px 2px #0001; /* optional, for subtle depth */
  }
</style>
