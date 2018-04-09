<template>
    <div>
        <navbar></navbar>
        <h2>Welcome to your dashboard</h2>
        <button @click="formBool = !formBool" class="new-form-button">Add a vault form</button>
        <div v-if="formBool == true">
            <form @submit.prevent="addVault(user)">
                <div class="form-group">
                    <label for="vault-name">Vault Name</label>
                    <input v-model="newVault.name" type="text" class="form-control" id="vault-name" aria-describedby="form name" placeholder="Name your vault">
                </div>
                <div class="form-group">
                    <label for="vault-description">Vault Description</label>
                    <input v-model="newVault.description" type="text" class="form-control" id="vault-description" placeholder="What goes in this vault?">
                </div>
                <button type="submit" class="btn btn-primary">Submit</button>
            </form>
        </div>
        <div class="list-group" v-for="vault in vaults">
            <a href="#" class="list-group-item list-group-item-action flex-column align-items-start">
                <div class="d-flex w-100 justify-content-between">
                    <router-link :to="{path: '/vault/' + vault.id }">
                        <h5 class="mb-1">{{vault.name}}</h5>
                    </router-link>
                </div>
                <p class="mb-1">{{vault.description}}</p>
            </a>
        </div>
    </div>
</template>

<script>
    import Navbar from './Navbar'
    export default {
        name: 'Dashboard',
        data() {
            return {
                formBool: false,
                newVault: {}
            }
        },
        mounted() {
            this.$store.dispatch('authenticate')
        },
        methods: {
            addVault(user) {
                this.$store.dispatch('addVault',
                    {
                        name: this.newVault.name,
                        description: this.newVault.description,
                        userId: user.id
                    })
            }
        },
        computed: {
            user() {
                return this.$store.state.user
            },
            vaults() {
                return this.$store.state.vaults
            }
        },
        components: {
            Navbar
        },
        params: ['vault']
    }
</script>

<style scoped>
    .list-group-item {
        margin-bottom: 2rem;
    }

    form {
        margin-bottom: 2rem;
    }

    .new-form-button {
        margin-bottom: 2rem;
    }
</style>