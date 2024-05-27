<script lang="ts">
    import Login from './pages/Login.svelte';
    export let url : string = '/fallback';
    let data : any = [];
    async function getData() : Promise<boolean> {
        const response = await fetch(url, {
            method: "GET",
            headers: {
                "Accept": "application/json",
            },
        });
        let status : number  = response.status;

        if (status != 200) {
            return false;    
        }
        data = await response.json();
        console.log(data);
        console.log('fine');
        return true;
    }
</script>

{#await getData()}
<div class="min-h-screen h-fit bg-gray-50">
  <div class="flex flex-col items-center my-6">
    <p class="mt-6 mb-2 text-4xl font-bold tracking-tight text-gray-900">Проверерка авторизации</p>
  </div>
</div>
{:then authenticated}
  {#if (authenticated) }
  <slot {data}></slot>
  {:else}
    <Login/>
  {/if}
{/await}