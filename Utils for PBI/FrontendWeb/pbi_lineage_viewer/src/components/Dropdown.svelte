<script>
    import {onMount} from 'svelte';

    export let apiUrl = '';
    export let placeholder = 'Select an object';
    export let keyField = 'id';
    export let valueField = 'value';

    let options = [];
    let selected = null;
    let isLoading = false;
    let error = null;

    onMount(async () => {
        if (!apiUrl){
            error = 'API URL is required';
            return;
        }
        isLoading = true;
        try {
            const response = await fetch(apiUrl);
            if(!response.ok) throw new Error(`Error: ${response.status}`);
            options = await response.json();
        } catch (err){
            error = err.message;
        } finally {
            isLoading = false;
        }
    });

    function handleSelect(event){
        selected = options.find(option => option[keyField] == event.target.value);
        dispatch('select', selected);
    }

    import {createEventDispatcher} from 'svelte';
    const dispatch = createEventDispatcher();

</script>

<div>
    <label>
        <select class="form-select" on:change={handleSelect} disabled={isLoading || error}>
            <option value="" disabled selected>{isLoading ? 'Loading...': placeholder}</option>
            {#each options as option}
                <option value={option[keyField]}>{option[valueField]}</option>
            {/each}
        </select>
    </label>
</div>