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
9
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
            options = apiData.map((option) => ({id: option[idKey], name: option[nameKey]}));
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

    export function filterObjects(filterColumn, filterValue){
        filteredOptions = apiData.filter((option) => option[filterColumn] == filterValue)
                                 .map((option) => ({id: option[idKey], name: option[nameKey]}));
    }

    function handleSelect(option){
        selectedValue = option.name;
        dispatch('select', option);
    }
    
    export function clearFilter(){
        filteredOptions = apiData.map((option) => ({id: option[idKey], name: option[nameKey]}));
        selectedValue = 'Select';
    }

</script>

<div class="container-mt-5 me-2 w-15">
    <Dropdown isOpen={dropdownOpen} toggle={() => (dropdownOpen = !dropdownOpen)}>
        <DropdownToggle caret>
            {selectedValue}
        </DropdownToggle>
        <DropdownMenu>
            <DropdownItem on:click={() => handleSelect({id:"CLEAR", name: "Clear"})}>
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