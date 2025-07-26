<script lang="ts">
    import APIDropdown from './APIDropdown.svelte';
    import { Table } from "@flowbite-svelte-plugins/datatable";
    import { baseURL } from '../ts/constant';
    import type { DataTableOptions } from "simple-datatables";
    let {closeModal} = $props();

    let objectTypeComponent;
    let objectsComponent;
    
    const onObjectTypeSelected = () => {

    }
    const onObjectSelected = () => {

    }

    const retrieveData = async () => {
        const tableInfoURL = baseURL + 'tables';

        const response = await fetch(tableInfoURL);

        return response.json();
    }

    const dataPromise = retrieveData();

    const paginationOptions: DataTableOptions = {
        paging: true,
        perPage: 7,
        sortable: true
    };

</script>
<div class="modal-backdrop" aria-label="Close" role="button">
<div class="modalbox-detailed">
    <button class="close-btn" onclick={closeModal} aria-label="Close">&times;</button>
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
    {#await dataPromise}
    <p>Loading table data...</p>
    {:then data}
    <div class="table-scroll">
    <Table items={data} striped dataTableOptions={paginationOptions} />
    </div>
    {/await}
</div>
</div>
<style>
:global(.data-table tbody){
    max-height: 60vh;
    overflow-y: auto;
}
:global(.datatable-table tbody tr:nth-child(even)) {
  background-color: #f5f5f5;
}
:global(.datatable-table td) {
  border-right: 1px solid #f3f2f2;
  border-left: 1px solid #f3f2f2;
}

:global(.datatable-table tbody tr:last-child ) {
  border-bottom: 1px solid #f3f2f2;
}
.modal-backdrop {
  position: fixed;
  top: 0; left: 0; right: 0; bottom: 0;
  background: rgba(0,0,0,0.25);
  display: flex;
  align-items: center;
  justify-content: center;
  z-index: 9999;
}
.modalbox-detailed {
  background: #fff;
  border-radius: 12px;
  box-shadow: 0 4px 32px #0003;
  padding: 24px 32px 18px 32px;
  width: 97vw;
  height: 97vh;
  max-width: 97vw;
  max-height: 97vh;
  position: relative;
  display: flex;
  flex-direction: column;
  align-items: flex-start;
  z-index: 10000;
}
.close-btn {
  position: absolute;
  top: 10px; right: 14px;
  background: none;
  border: none;
  font-size: 2em;
  color: #888;
  cursor: pointer;
}
.close-btn:hover { color: #d32f2f; }
</style> 