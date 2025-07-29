<script lang="ts">
  import { SvelteFlowProvider } from '@xyflow/svelte';
  import Flow from './components/Flow.svelte';
  import APIDropdown from './components/APIDropdown.svelte';

  let flowComponent, dropdownObjectComponent;

  const onObjectSelected = (selectedValue) => {
    console.log(selectedValue);
    flowComponent.filterNode(selectedValue);
  }
  
</script>
<div class="corner-dropdown">
<APIDropdown
  dropdownName = 'Objects'
  apiUrl = "http://localhost:8080/utilspbi/api/nodesinfo"
  idKey = "id"
  nameKey = "name"
  enableFiltering = {true}
  bind:this = {dropdownObjectComponent}
  handleDropdownSelect = {onObjectSelected}
/>
</div>
<SvelteFlowProvider>
  <Flow bind:this={flowComponent}/>
</SvelteFlowProvider>

<style>
.corner-dropdown {
  position: absolute;
  top: 18px;
  left: 24px;
  z-index: 1001;
}
</style>