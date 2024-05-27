<script lang="ts">
    import { Router, Route } from 'svelte-routing';
    import Login from './pages/Login.svelte';
    import Register from './pages/Register.svelte';
    import Main from './pages/Main.svelte';
    import AddVideo from './pages/AddVideo.svelte';
    import DelteVideo from './pages/DelteVideo.svelte';
    import Favourite from './pages/Favourite.svelte';
    import Admin from './pages/Admin.svelte';
</script>


  <Router>
    <Route path="/login" component={Login}/>
    <Route path="/register" component={Register}/>
    <Route path="/favourite" component={Favourite}/>
    <Route path="/addVideo" component={AddVideo}/>
    <Route path="/deleteVideo" component={DelteVideo}/>
    <Route path="/admin" component={Admin}/>
    <Route component={Main}/>
  </Router>


