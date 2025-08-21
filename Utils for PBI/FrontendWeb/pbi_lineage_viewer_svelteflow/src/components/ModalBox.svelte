<script lang="ts">
    import {formatDAX} from "../ts/utility-formatter"; 
    let {closeModal, modalData} = $props();
    
</script>

    <div class="modal-backdrop" aria-label="Close" role="button">
    <div class="modal-box" onclick={(event) => {event.stopPropagation}} aria-label="Close">
        <button class="close-btn" onclick={closeModal} aria-label="Close">&times;</button>
        <div class="modal-content">
        <h3>{modalData.Name}</h3>
        <pre>{formatDAX(modalData.Expression)}</pre>
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
.modal-box {
  background: #fff;
  border-radius: 12px;
  box-shadow: 0 4px 32px #0003;
  padding: 24px 32px 18px 32px;
  min-width: 320px;
  max-width: 90vw;
  max-height: 80vh;
  position: fixed;
  display: flex;
  flex-direction: column;
  align-items: flex-end;
  z-index: 10000;
}
.modal-content pre {
  background: #f5f5f5;
  color: #222;
  border-radius: 6px;
  padding: 12px 16px;
  font-family: 'Fira Mono', 'Consolas', 'Menlo', monospace;
  font-size: 1em;
  margin: 0;
  overflow-x: auto;
  box-shadow: 0 1px 4px #0001;
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
.modal-content {
  width: 100%;
  margin-top: 18px;
  text-align: left;
  color: #222;
  font-size: 1em;
  overflow: auto;
}
</style>