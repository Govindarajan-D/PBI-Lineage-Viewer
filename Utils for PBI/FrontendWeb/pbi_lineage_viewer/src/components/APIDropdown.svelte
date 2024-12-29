<script>
    import {onMount} from 'svelte';
    import {Dropdown, DropdownMenu, DropdownItem, DropdownToggle} from 'sveltestrap';
    import {createEventDispatcher} from 'svelte';

    export let apiUrl = '';
    export let dropdownName = '';
    export let enableFiltering = false;

    let options = [];
    let filteredOptions = [];
    let placeholder = 'Select';
    let dropdownSearchQuery = '';
    let dropdownOpen = false;
    let isLoading = false;
    let error = null;

    const dispatch = createEventDispatcher();

    onMount(async () => {
        if (!apiUrl){
            error = 'API URL is required';
            return;
        }
        isLoading = true;
        try {
            const response = await fetch(apiUrl);
            if(!response.ok) throw new Error(`Error: ${response.status}`);
            const data = await response.json();
            options = data.map((option) => ({id: option.id, name: option.name}));
            filteredOptions = options;
        } catch (err){
            error = err.message;
        } finally {
            isLoading = false;
        }
    });

    $:{
            filteredOptions = options.filter(option => 
                    option.name?.toString().toLowerCase().includes(dropdownSearchQuery.toLowerCase())
            );

    }


    function handleSelect(option){
        dispatch('select', option);
    }

</script>

<div class="container-mt-5">
    <Dropdown isOpen={dropdownOpen} toggle={() => (dropdownOpen = !dropdownOpen)}>
        <DropdownToggle caret>
            {placeholder}
        </DropdownToggle>
        <DropdownMenu>
            {#if enableFiltering}
             <div class="p-2">
                <input
                    type="text"
                    class="form-control"
                    placeholder="Search..."
                    bind:value={dropdownSearchQuery}
                />
            </div>
            {/if}
        {#if filteredOptions.length > 0}
        {#each filteredOptions as option}
            <DropdownItem on:click={() => handleSelect(option)}>
                {option.name}
            </DropdownItem>
        {/each}
        {:else}
            <DropdownItem disabled>No results found</DropdownItem>
        {/if}
    </DropdownMenu>
    </Dropdown>
</div>