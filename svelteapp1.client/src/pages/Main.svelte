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
    function favourite(){
        window.location.href = "/favourite";
    }
    async function search(){
        let url = "/fallback/videos?";
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

<Authorize let:data url={"/fallback/videos"}>
    <main class="flex flex-col items-stretch justify-center">
        <div class="w-full grid grid-cols-9 my-4">
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
            <button class="justify-self-end mx-10 col-span-1" on:click|preventDefault={favourite}>
                <svg class="hover:fill-slate-600 self-center" fill="#000000" height="24" width="24" version="1.1" id="Capa_1" xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" 
                viewBox="0 0 471.701 471.701" xml:space="preserve">
                <g>
                    <path d="M433.601,67.001c-24.7-24.7-57.4-38.2-92.3-38.2s-67.7,13.6-92.4,38.3l-12.9,12.9l-13.1-13.1
                        c-24.7-24.7-57.6-38.4-92.5-38.4c-34.8,0-67.6,13.6-92.2,38.2c-24.7,24.7-38.3,57.5-38.2,92.4c0,34.9,13.7,67.6,38.4,92.3
                        l187.8,187.8c2.6,2.6,6.1,4,9.5,4c3.4,0,6.9-1.3,9.5-3.9l188.2-187.5c24.7-24.7,38.3-57.5,38.3-92.4
                        C471.801,124.501,458.301,91.701,433.601,67.001z M414.401,232.701l-178.7,178l-178.3-178.3c-19.6-19.6-30.4-45.6-30.4-73.3
                        s10.7-53.7,30.3-73.2c19.5-19.5,45.5-30.3,73.1-30.3c27.7,0,53.8,10.8,73.4,30.4l22.6,22.6c5.3,5.3,13.8,5.3,19.1,0l22.4-22.4
                        c19.6-19.6,45.7-30.4,73.3-30.4c27.6,0,53.6,10.8,73.2,30.3c19.6,19.6,30.3,45.6,30.3,73.3
                        C444.801,187.101,434.001,213.101,414.401,232.701z"/>
                </g>
                </svg>
            </button>
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
                    {#each data as video}
                        <Video {...video}/>
                    {/each}
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
