<script lang="ts">
  let { additionalData } = $props();
</script>
  <div class="mui-node-extra">
  <table class="mui-node-table">
    <tbody>
    <tr>
      <td class="label">Hidden</td>
      <td>{additionalData.IsHidden}</td>
    </tr>
    <tr>
      <td class="label">Modified Time</td>
      <td>{additionalData.ModifiedTime}</td>
    </tr>
    <tr>
      <td class="label">Display Folder</td>
      <td>{additionalData.DisplayFolder}</td>
    </tr>
    </tbody>
  </table>
</div>

<style>

.mui-node-extra {
    padding: 10px 18px;
    background: #f9f9f9;
    border-top: 1px solid #eee;
    font-size: 1.1em;
    color: #444;
  }
  .mui-node-table {
    width: 100%;
    border-collapse: collapse;
    font-size: 1em;
  }
  .mui-node-table td {
    padding: 4px 8px;
    border: none;
    vertical-align: top;
  }
  .mui-node-table .label {
    font-weight: bold;
    color: #1976d2;
    width: 30%;
    white-space: nowrap;
    padding-right: 12px;
  }
</style>
