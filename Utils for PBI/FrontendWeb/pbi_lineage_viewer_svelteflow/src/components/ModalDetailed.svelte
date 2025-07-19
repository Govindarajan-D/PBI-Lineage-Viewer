<script lang="ts">
    import APIDropdown from './APIDropdown.svelte';
    let {closeModal} = $props();

    let objectTypeComponent;
    let objectsComponent;
    

    const onObjectTypeSelected = () => {

    }
    const onObjectSelected = () => {

    }

</script>
<div class="modal-backdrop" aria-label="Close" role="button">
<div class="modalbox-detailed">
    <button class="close-btn" onclick={closeModal} aria-label="Close">&times;</button>
    <h3>
        Detailed Metadata
    </h3>
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
</div>
</div>
<style>
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