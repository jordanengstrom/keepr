<template>
    <nav class="navbar bg-light flexor">
        <div>
            <router-link :to="{name: 'Home'}">
                <button type="button" class="btn btn-secondary">Keepr</button>
            </router-link>
        </div>
        <div class="d-flex">
            <button type="button" class="btn btn-secondary logout-btn" @click="logout()">Logout</button>
            <router-link :to="{name: 'Dashboard'}">
                <button type="button" class="btn btn-secondary">Dashboard</button>
            </router-link>
        </div>
        </div>
        </div>
    </nav>
</template>


<script>
    export default {
        name: 'Navbar',
        data() {
            return {
            }
        },
        methods: {
            logout() {
                this.$store.dispatch('logout');
            }
        }
    }
</script>

<style scoped>
    .logout-btn {
        margin-right: 1rem;
    }

    .d-flex {
        align-items: center
    }

    .flexor {
        display: flex;
        align-items: flex-end;
        justify-content: space-between;
    }
</style>