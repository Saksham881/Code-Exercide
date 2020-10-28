<template>
    <div id="app">
        <b-container>
            <router-view>
            
            </router-view>
        </b-container>
    </div>

</template>

<script>
    import Home from './components/Home.vue';
    import NewBook from './components/NewBook.vue';

    export default {
        name: 'app',
        components: {
            Home,
            NewBook
        }
    };
</script>

<style>
</style>

