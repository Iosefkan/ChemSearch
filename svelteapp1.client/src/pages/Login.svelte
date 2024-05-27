<script lang="ts">
    let name : string;
    let password : string;
    let isMessage: boolean = false;
    let message: string = "Необходимо заполнить поля";
    function validate(){
        if (!name){
            isMessage = true;
            message = "Необходимо заполнить поля";
            return;
        }
        if (!password){
            isMessage = true;
            message = "Необходимо заполнить поля";
            return;
        }
        isMessage = false;
    }
    async function login(){
        validate();
        if (isMessage){
            return;
        }
        const data = { 
            username: name,
            password: password,
        };
        let url = "/fallback/auth/login";
        const response = await fetch(url, {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
            },
            body: JSON.stringify(data),
        });
        const result = await response;
        const jsonData = await result.json();
        if (result.ok) {
            if (jsonData.isAdmin){
                window.location.href = "/admin";
                return;
            }
            window.location.href = "/";
        }
        else {
            isMessage = true;
            message = "Ошбика входа";
        }
    }
</script>

<main class="h-screen flex flex-col items-center">
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
    <div class='w-1/3 my-2 flex flex-col items-stetch'>
        <label for='name' class="mb-2">Имя</label>
        <input bind:value={name} id='name' type="text" placeholder="ИмяПользователя" class="rounded-md h-10 ring-1  ring-black"/>
    </div>
    <div class='w-1/3 my-2 mb-4 flex flex-col items-stetch'>
        <label for='pas' class="mb-2">Пароль</label>
        <input bind:value={password} id='pas' type="password" placeholder="••••••••" class="rounded-md h-10 ring-1  ring-black"/>
    </div>
    <button on:click|preventDefault={login} class="rounded-md bg-[#28c641] my-3 h-10 w-1/3">
        Войти
    </button>
    <a href="/register" class="text-center hover:shadow hover:text-cyan-600">
        Зарегистрироваться
    </a>
    {#if (isMessage)}
    <p class="text-center">
        {message}
    </p>
    {/if}
</main>