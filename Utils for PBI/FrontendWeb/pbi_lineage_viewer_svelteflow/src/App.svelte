<script lang="ts">
  import { SvelteFlowProvider } from '@xyflow/svelte';
 
  import Flow from './components/Flow.svelte';
</script>
 
<SvelteFlowProvider>
  <Flow />
</SvelteFlowProvider>