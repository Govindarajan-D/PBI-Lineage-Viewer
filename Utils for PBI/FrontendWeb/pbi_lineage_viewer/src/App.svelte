<script>
    import 'bootstrap/dist/css/bootstrap.min.css';
    import Cytoscape from './components/Cytoscape.svelte';
    import APIDropdown from './components/APIDropdown.svelte';

    let objectTypeComponent;
    let objectsComponent;
    let cytoscapeComponent;
    function onObjectTypeSelected(event){
        if(objectsComponent){
          objectsComponent.filterObjects("objectTypeID", event.detail.id);
        }
    }
    function onObjectSelected(event) {
        if(cytoscapeComponent){
          cytoscapeComponent.filterCytoscapeNode(event.detail.id);
        }
    }
</script>
<div class="container-fluid full-height d-flex flex-column">
    <div class="top-bar d-flex align-items-center justify-content-start">
            <APIDropdown
                dropdownName = 'ObjectType'
                apiUrl = "http://localhost:8080/utilspbi/api/objecttypeinfo"
                idKey = "objectTypeID"
                nameKey = "objectTypeName"
                enableFiltering = {false}
                bind:this = {objectTypeComponent}
                on:select = {onObjectTypeSelected}
            />
            <APIDropdown
                dropdownName = 'Objects'
                apiUrl = "http://localhost:8080/utilspbi/api/nodesinfo"
                idKey = "id"
                nameKey = "name"
                enableFiltering = {true}
                bind:this = {objectsComponent}
                on:select = {onObjectSelected}
            />
    </div>
    <div class="cytoscape-container">
        <Cytoscape bind:this = {cytoscapeComponent}>
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
