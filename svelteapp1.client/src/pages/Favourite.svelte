<script lang="ts">
    import Video from "../components/Video.svelte";
    import Accordion from "../components/Accordion.svelte";
    import Authorize from "../Authorize.svelte";
    import { onMount } from "svelte";
    let gradeGroup: any = [];
    let topicGroup: any = [];
    let name: string;
    let searchData: any = [];
    let grades: any = [];
    let topics: any = [];
    let searched: boolean = false;
    let empty: boolean = false;

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
    async function search(){
        let url = "/fallback/videos/favourite?";
        for (let i = 0; i < gradeGroup.length; i++){
            url += `gradeId=${gradeGroup[i]}&`;
        }
        for (let i = 0; i < topicGroup.length; i++){
            url += `topicid=${topicGroup[i]}&`;
        }
        if (name){
            url += `name=${name}`;
        }
        const response = await fetch(url, {
            method: "GET",
            headers: {
                "Accept": "application/json",
            },
        });
        const result = await response;
        searchData = await result.json();
        if (result.ok) {
            if (searchData.isEmpty){
                empty = true;
            }
            else {
                empty = false;
            }
            searched = true; 
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

<Authorize let:data url={"/fallback/videos/favourite"}>
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
            <div class="ring-1 ring-black rounded-md col-span-5 my-2 grid grid-cols-8 items-center ">
                <input bind:value={name} placeholder="Название" class="mx-2 focus:ring-0 outline-none col-span-7"/>
                <button class="justify-self-end mr-2" on:click|preventDefault={search}>
                    <svg fill="#000000" height="24" width="24" version="1.1" id="Capa_1" xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" 
                        viewBox="0 0 488.4 488.4" xml:space="preserve">
                    <g>
                        <g>
                            <path d="M0,203.25c0,112.1,91.2,203.2,203.2,203.2c51.6,0,98.8-19.4,134.7-51.2l129.5,129.5c2.4,2.4,5.5,3.6,8.7,3.6
                                s6.3-1.2,8.7-3.6c4.8-4.8,4.8-12.5,0-17.3l-129.6-129.5c31.8-35.9,51.2-83,51.2-134.7c0-112.1-91.2-203.2-203.2-203.2
                                S0,91.15,0,203.25z M381.9,203.25c0,98.5-80.2,178.7-178.7,178.7s-178.7-80.2-178.7-178.7s80.2-178.7,178.7-178.7
                                S381.9,104.65,381.9,203.25z"/>
                        </g>
                    </g>
                    </svg>
                </button>
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
                    Фильтрация по:
                </div>
                <Accordion>
                    <span slot="head" class="mx-3 font-sans">Класс</span>
                    <div slot="details" class="flex flex-col items-justify mx-3 font-sans">
                        {#each grades as grade}
                            <label>
                                <input
                                    type="checkbox"
                                    name="gradeId"
                                    value={grade.id}
                                    class="accent-[#28c641]"
                                    bind:group={gradeGroup}
                                />
                                {grade.gradeNumber} класс
                            </label>
                        {/each}
                    </div>
                </Accordion>
                <Accordion>
                    <span slot="head" class="mx-3 font-sans">Тема</span>
                    <div slot="details" class="flex flex-col items-justify mx-3 font-sans">
                        {#each topics as topic}
                            <label>
                                <input
                                    type="checkbox"
                                    name="topicId"
                                    value={topic.id}
                                    class=" checked:bg-[#28c641]"
                                    bind:group={topicGroup}
                                />
                                {topic.name}
                            </label>
                        {/each}
                    </div>
                </Accordion>
                
                
            </div>
            <div class="grid grid-cols-2 justify-items-center mx-6 w-5/6">
                {#if (!searched)}
                    {#if (!data.length || data.length === 0)}
                        <p>В вашем избранном нет видео</p>
                    {:else}
                        {#each data as video}
                            <Video {...video}/>
                        {/each}
                    {/if}
                    
                {:else}
                    {#await searchData}
                        <p>Получение данных</p>
                    {:then res} 
                        {#if (empty)}
                            <p>
                                Видео по данным критериям не найдены.
                            </p>
                        {:else}
                            {#each searchData as video}
                                <Video {...video}/>
                            {/each}
                        {/if}
                    {/await}
                {/if}
            </div>
        </div>
    </main>
</Authorize>
