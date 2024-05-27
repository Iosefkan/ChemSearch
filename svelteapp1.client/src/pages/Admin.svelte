<script lang="ts">
    import Accordion from "../components/Accordion.svelte";
    let isMessage: boolean = false;
    let newTopic: string;
    let message: string = "Заполните поле";
    function validate(){
        if (!newTopic){
            isMessage = true;
            message = "Заполните поле";
            return;
        }
        isMessage = false;
    }
    function add(){
        document.location.href = "/addVideo";
    }
    function del(){
        document.location.href = "/deleteVideo";
    }
    async function addTopic(){
        validate();
        if (isMessage){
            return;
        }
        const data = { 
            topic: newTopic,
        };
        let url = "/fallback/topic/add";
        const response = await fetch(url, {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
            },
            body: JSON.stringify(data),
        });
        const result = await response;
        if (result.ok) {
            isMessage = true; 
            message = "Добавление успешно";
        }
        else {
            isMessage = true; 
            message = "Ошбика добавления";
        }
    }
</script>

<main class="top-0 bottom-0 h-screen flex flex-col items-center">
    <div class=" mb-14 mt-4">
        <div class="font-sans italic font-bold text-7xl text-center">
            <span class="text-green-800">chem</span>
            <span class="text-[#28c641]">search</span>
        </div>
        <p class="text-center text-2xl">
            видео по химии для школьников
        </p>
    </div>
    <svg class="feather feather-user mb-1" fill="none" height="50" stroke="currentColor" stroke-linecap="round" stroke-linejoin="round" stroke-width="2" viewBox="0 0 24 24" width="50" xmlns="http://www.w3.org/2000/svg">
        <path d="M20 21v-2a4 4 0 0 0-4-4H8a4 4 0 0 0-4 4v2"/>
        <circle cx="12" cy="7" r="4"/>
    </svg>
    <button on:click|preventDefault={add} class="rounded-md bg-[#28c641] my-3 h-10 w-1/3">
        Добавить видео
    </button>
    <button on:click|preventDefault={del} class="rounded-md bg-[#28c641] my-3 h-10 w-1/3">
        Удалить видео
    </button>
    <Accordion full={true}>
        <span slot="head" class="mx-3 font-sans">Добавить тему</span>
        <div slot="details" class="flex flex-col items-justify mx-3 font-sans">
            <input type="text" placeholder="Новая тема" bind:value={newTopic} class="ring-1 ring-black rounded-md w-full p-2"/>
            <button on:click|preventDefault={addTopic} class="rounded-md bg-[#28c641] my-3 h-10 w-full">
                Добавить тему
            </button>
            {#if (isMessage)}
                <p class="text-wrap">
                    {message}
                </p> 
            {/if}
        </div>
    </Accordion>
</main>