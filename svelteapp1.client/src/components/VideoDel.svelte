<script lang="ts">
    export let id: number;
    export let title: string;
    export let grade: number;
    export let topic: string;
    export let link: string;
    async function Delete(){
        let data = {
            id: id,
        }
        let url = "/fallback/videos/delete";
        const response = await fetch(url, {
            method: "DELETE",
            headers: {
                "Content-Type" : "application/json",
            },
            body: JSON.stringify(data)
        });
        const result = await response;
        if (result.ok) {
            window.location.reload();
        }
        else {
            alert("Ошибка");
        }
    }
    let videoLink = link.replace('watch?v=', 'embed/')
</script>

<div class="flex flex-col items-justify shadow-sm w-fit">
    <p class="my-2 text-wrap text-xl">
        {title}
    </p>
    <iframe 
        src={videoLink} 
        width="448" 
        height="252" 
        frameborder="0" 
        allowfullscreen>
    </iframe>
    <ul>
        <li>{grade} класс</li>
        <li class="text-wrap">{topic}</li>
    </ul>
    <button on:click|preventDefault={Delete} class="rounded-md bg-[#28c641] my-3 h-10">
        Удалить
    </button>

</div>