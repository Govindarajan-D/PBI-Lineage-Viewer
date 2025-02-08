<script>
    import {onMount} from 'svelte';
    import {Dropdown, DropdownMenu, DropdownItem, DropdownToggle} from 'sveltestrap';
    import {createEventDispatcher} from 'svelte';

    export let apiUrl = '';
    export let dropdownName = '';
    export let idKey;
    export let nameKey;
    export let enableFiltering = false;

    let options = [];
    let filteredOptions = [];
    let selectedValue = 'All';
    let dropdownSearchQuery = '';
    let dropdownOpen = false;
    let isLoading = false;
    let error = null;

    let apiData;

    const dispatch = createEventDispatcher();
    
    // On initialization - Hit the API Endpoint and load the dropdown with key-value pairs
    // The Key and Value is determined by the params passed in the Parent HTML tag <APIDropdown>
    onMount(async () => {
        if (!apiUrl){
            error = 'API URL is required';
            return;
        }
        isLoading = true;
        try {
            const response = await fetch(apiUrl);
            if(!response.ok) throw new Error(`Error: ${response.status}`);
            apiData = await response.json();
            options = apiData.map((option) => ({id: option[idKey], name: option[nameKey]}))
                             .sort((a,b) => a.name.toLowerCase().localeCompare(b.name.toLowerCase()));
            filteredOptions = options;
        } catch (err){
            error = err.message;
        } finally {
            isLoading = false;
        }
    });

    // React to the search query typed in the Search box
    $:{
            filteredOptions = options.filter(option => 
                    option.name?.toString().toLowerCase().includes(dropdownSearchQuery.toLowerCase())
            );
    }

    // Apply filter based on the selected value (from an external source like another dropdown)
    export function filterObjects(filterColumn, filterValue){
        filteredOptions = apiData.filter((option) => option[filterColumn] == filterValue)
                                 .map((option) => ({id: option[idKey], name: option[nameKey]}));
    }

    function handleSelect(option){
        selectedValue = option.name;
        dispatch('select', option);
    }
    
    export function clearFilter(){
        selectedValue = 'All';
        filteredOptions = apiData.map((option) => ({id: option[idKey], name: option[nameKey]}));
    }

</script>

<div class="container-mt-5 me-2 w-15">
    <Dropdown isOpen={dropdownOpen} toggle={() => (dropdownOpen = !dropdownOpen)}>
        <DropdownToggle caret>
            {selectedValue}
        </DropdownToggle>
        <DropdownMenu class="dropdown-scroll">
            <DropdownItem on:click={() => handleSelect({id:"SIG_DD_CLEAR", name: "Clear"})}>
                Clear Filter
            </DropdownItem>
            <DropdownItem divider />
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
<style>
/* Global needs to be used as Svelte does not recognize classes applied on custom components (DropdownMenu). It will work in native HTML tags like div */
:global(.dropdown-scroll){
    max-height: 300px;
    overflow-y: auto;
    border: 1px solid #ddd;
}
:global(.dropdown-scroll::-webkit-scrollbar) {
    width: 8px; /* Width of the scrollbar */
}

:global(.dropdown-scroll::-webkit-scrollbar-thumb) {
    background: #888; /* Scrollbar color */
    border-radius: 4px; /* Rounded edges */
}

:global(.dropdown-scroll::-webkit-scrollbar-thumb:hover) {
    background: #555; /* Darker on hover */
}

:global(.dropdown-scroll::-webkit-scrollbar-track) {
    background: #f1f1f1; /* Background of scrollbar */
    border-radius: 4px;
}

</style>