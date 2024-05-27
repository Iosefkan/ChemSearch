<script lang="ts">
    import Accordion from "../components/Accordion.svelte";
    import { onMount } from "svelte";
    let gradeGroup: any;
    let topicGroup: any;
    let newTitle: string;
    let newLink: string;
    let error: boolean;
    let message: string;
    let grades: any = [];
    let topics: any = [];

    async function logout(){
        const response = await fetch("/fallback/auth/logout", {
            method: "GET"
        });
        const result = await response;
        if (result.ok){
            window.location.href = "/login";
        }
        else{
            alert("Ошибка при выходе");
        }
    }
    function validate(){
        if (!newTitle || !newLink || !topicGroup || !gradeGroup){
            error = true;
            message = "Заполните все поля";
            return;
        }
        error = false;
    }
    async function add(){
        validate();
        if (error){
            return;
        }
        const data = { 
            link: newLink,
            title: newTitle,
            topicId: topicGroup,
            gradeId: gradeGroup,
        };
        let url = "/fallback/videos/add";
        const response = await fetch(url, {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
            },
            body: JSON.stringify(data),
        });
        const result = await response;
        if (result.ok) {
            error = true; 
            message = "Видео успешно добавлено";
        }
        else {
            error = true; 
            message = "Ошбика добавления";
        }
    }
    onMount(async() => {
        const resultTopic = await fetch("/fallback/topic", {
            method: "GET",
            headers: {
                "Accept" : "application/json",
            }
        });
        topics = await resultTopic.json();
        const resultGrade = await fetch("/fallback/grade", {
            method: "GET",
            headers: {
                "Accept" : "application/json",
            }
        });
        grades = await resultGrade.json();
    })
</script>

<main class="flex flex-col items-stretch justify-center">
    <div class="w-full grid grid-cols-8 my-4">
        <div class="col-span-2 justify-self-start mx-5">
            <div class=" font-sans italic font-bold text-4xl text-center">
                <span class="text-green-800">chem</span>
                <span class="text-[#52935c]">search</span>
            </div>
            <p class="text-center text-sm">
                видео по химии для школьников
            </p>
        </div>
        <div class="col-span-5 my-2">
        </div>
        <button class="justify-self-end mx-10 col-span-1" on:click|preventDefault={logout}>
            <svg class="feather feather-user hover:fill-slate-600 self-center" fill="none" height="24" stroke="currentColor" stroke-linecap="round" stroke-linejoin="round" stroke-width="2" viewBox="0 0 24 24" width="24" xmlns="http://www.w3.org/2000/svg">
                <path d="M20 21v-2a4 4 0 0 0-4-4H8a4 4 0 0 0-4 4v2"/>
                <circle cx="12" cy="7" r="4"/>
            </svg>
        </button>
    </div>
    <div class="w-full h-full flex flex-row">
        <div class="top-0 bottom-0 overflow-y-auto overflow-x-hidden flex flex-col items-justify mx-2 w-1/6">
            <div class="my-3 mx-3 font-sans">
                Добавить видео в раздел:
            </div>
            <Accordion>
                <span slot="head" class="mx-3 font-sans">Класс</span>
                <div slot="details" class="flex flex-col items-justify mx-3 font-sans">
                    {#await grades}
                        <p></p>
                    {:then res} 
                        {#each grades as grade}
                            <label>
                                <input
                                    type="radio"
                                    name="gradeId"
                                    value={grade.id}
                                    class="accent-[#28c641]"
                                    bind:group={gradeGroup}
                                />
                                {grade.gradeNumber} класс
                            </label>
                        {/each}
                    {/await}
                    
                </div>
            </Accordion>
            <Accordion>
                <span slot="head" class="mx-3 font-sans">Тема</span>
                <div slot="details" class="flex flex-col items-justify mx-3 font-sans">
                    {#await topics}
                        <p></p>
                    {:then res} 
                        {#each topics as topic}
                            <label>
                                <input
                                    type="radio"
                                    name="topicId"
                                    value={topic.id}
                                    class=" checked:bg-[#28c641]"
                                    bind:group={topicGroup}
                                />
                                {topic.name}
                            </label>
                        {/each}
                    {/await} 
                </div>
            </Accordion>
            
            
        </div>
        <div class="flex flex-col items-center mx-6 w-5/6 h-full">
            <input bind:value={newTitle} placeholder="Название на сайте" class="p-2 w-1/2 ring-1 ring-black rounded-md my-4"/>
            <input bind:value={newLink} placeholder="Ссылка" class="p-2 w-1/2 ring-1 ring-black rounded-md my-4"/>
            <button on:click|preventDefault={add} class="rounded-md bg-[#28c641] my-3 h-10 w-1/2">Добавить</button>
            {#if (error)}
                <p class="my-4">
                    {message}
                </p>
            {/if}
        </div>
    </div>
</main>