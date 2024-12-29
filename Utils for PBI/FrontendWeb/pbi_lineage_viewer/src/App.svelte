<script>
    import 'bootstrap/dist/css/bootstrap.min.css';
    import Cytoscape from './components/Cytoscape.svelte';
    import APIDropdown from './components/APIDropdown.svelte';

    function handleSelection(event) {
        console.log('Selected Option:', event.detail);
    }
</script>
<div class="container-fluid full-height d-flex flex-column">
    <div class="top-bar d-flex align-items-center justify-content-start">
            <APIDropdown
                dropdownName = 'ObjectType'
                apiUrl="https://jsonplaceholder.typicode.com/users"
                enableFiltering = true
                on:select={handleSelection}
            />
    </div>
    <div class="cytoscape-container">
        <Cytoscape>
        </Cytoscape>
    </div>
</div>
<style>
  .top-bar {
    height: 15%;
    background-color: #f8f9fa; /* Light gray background */
  }
  .cytoscape-container {
    height: 85%;
    overflow: hidden;
  }

  .full-height {
    height: 100vh;
  }
</style>
