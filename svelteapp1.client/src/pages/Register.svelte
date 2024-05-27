<script lang="ts">
    let name : string;
    let password : string;
    let passwordAgain: string;
    let showMessage: boolean = false;
    let message: string = "Необходимо заполнить поля";
    function validate(){
        if (!name || !password || !passwordAgain){
            showMessage = true;
            message = "Необходимо заполнить поля";
            return;
        }
        if (password != passwordAgain){
            showMessage = true;
            message = "Необходимо заполнить поля";
            return;
        }
        showMessage = false;
    }
    async function register(){
        validate();
        if (showMessage){
            return;
        }
        const data = { 
            username: name,
            password: password,
        };
        let url = "/fallback/auth/register";
        const response = await fetch(url, {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
            },
            body: JSON.stringify(data),
        });
        const result = await response;
        if (result.ok) {
            showMessage = true; 
            message = "Регистрация прошла успешно";
        }
        else {
            showMessage = true; 
            message = "Ошбика регистрации";
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
    <div class='w-1/3 my-2 mb-4 flex flex-col items-stetch'>
        <label for='pasAg' class="mb-2">Пароль повторно</label>
        <input bind:value={passwordAgain} id='pasAg' type="password" placeholder="••••••••" class="rounded-md h-10 ring-1  ring-black"/>
    </div>
    <button on:click|preventDefault={register} class="rounded-md bg-[#28c641] my-3 h-10 w-1/3">
        Зарегистрироваться
    </button>
    <a href="/login" class="text-center hover:shadow hover:text-cyan-600">
        Логин
    </a>
    {#if (showMessage)}
    <p class="text-center text-red-600">
        {message}
    </p>
    {/if}
</main>