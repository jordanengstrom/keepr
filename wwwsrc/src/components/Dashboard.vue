<template>
    <div>
        <navbar></navbar>
        <h2>Welcome to your dashboard</h2>
        <button @click="formBool = !formBool">Add a vault</button>
        <div v-if="formBool == true">
            <form @submit.prevent="addVault()">
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
        methods: {
            addVault() {
                this.$store.dispatch('addVault', this.newVault)
            }
        },
        components: {
            Navbar
        }
    }
</script>

<style scoped>
</style>