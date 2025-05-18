<script>
    import 'bootstrap/dist/css/bootstrap.min.css';
    import Cytoscape from './components/Cytoscape.svelte';
    import APIDropdown from './components/APIDropdown.svelte';

    let objectTypeComponent;
    let objectsComponent;
    let cytoscapeComponent;

    /* 
       Multiple Object filter interaction functions. Based on the selected value the other dropdown and cytoscape object is filtered/cleared 
       Signals are passed from child components for interaction. 
          SIG_DD_CLEAR - Clear signal comes from the dropdown component 
          SIG_CY_CLEAR_DROPDOWN - Clear signal comes from the Cytoscape component
       This is done to avoid circular function calls

       Based on the meta data, two dropdowns are available:
        ObjectType - Measure, Table, Calc. Column, Column
        Object - Actual names of the objects (for e.g Sales YTD, Sum of Quantity)
    */
   
    function onObjectTypeSelected(event){
      if(event.detail.id == "SIG_DD_CLEAR"){
        objectTypeComponent.clearFilter();
        objectsComponent.clearFilter();
        cytoscapeComponent.clearFilter();
      }
      else if(event.detail.id == "SIG_CY_CLEAR_DROPDOWN"){
        objectTypeComponent.clearFilter();
        objectsComponent.clearFilter();
      }
      else{
        if(objectsComponent){
          objectsComponent.filterObjects("objectTypeID", event.detail.id);
        }
      }
    }
    
    function onObjectSelected(event) {
      if(event.detail.id == "SIG_DD_CLEAR"){
        objectsComponent.clearFilter();
        cytoscapeComponent.clearFilter();
      }
      else{
        if(cytoscapeComponent){
          cytoscapeComponent.filterCytoscapeNode(event.detail.id);
        }
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
        <Cytoscape bind:this = {cytoscapeComponent} on:clear_dropdown={onObjectTypeSelected}>
        </Cytoscape>
    </div>
</div>
<style>
  :global(body) { /* this will apply to <body> */ margin: 0; padding: 0; }
  .top-bar {
    height: 15%;
    background-color: #1d2836; /* Light gray background */
  }
  .cytoscape-container {
    height: 85%;
    overflow: hidden;
    background-color: #1d2836;
  }

  .full-height {
    height: 100vh;
  }
</style>
