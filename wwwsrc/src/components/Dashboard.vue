<template>
    <div>
        <navbar></navbar>
        <h2>Welcome to your dashboard</h2>
    </div>
</template>

<script>
    import Navbar from './Navbar'
    export default {
        name: 'Dashboard',
        data() {
            return {
            }
        },
        components: {
            Navbar
        }
    }
</script>

<style>
</style>