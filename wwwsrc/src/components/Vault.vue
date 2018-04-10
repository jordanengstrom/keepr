<template>
    <div>
        <navbar></navbar>
        <h1>{{vault.name}}</h1>
        <h3>{{vault.description}}</h3>
        <button @click="formBool = !formBool" class="new-form-button">Add keep form</button>
        <div v-if="formBool == true">
            <form @submit.prevent="addKeep(vault)">
                <div class="form-group">
                    <label for="keep-name">Keep Image Link</label>
                    <input v-model="newKeep.img" type="text" class="form-control" id="keep-img" aria-describedby="form name" placeholder="Link to image of content">
                </div>
                <div class="form-group">
                    <label for="keep-link">keep link</label>
                    <input v-model="newKeep.link" type="text" class="form-control" id="keep-description" placeholder="Link to content">
                </div>
                <div class="form-group">
                    <label for="keep-description">keep description</label>
                    <input v-model="newKeep.description" type="text" class="form-control" id="keep-description" placeholder="Blog about the keep">
                </div>
                <button type="submit" class="btn btn-primary">Submit</button>
            </form>
        </div>
        <!-- <div v-for="keep in keeps" :vault="vault" :keep="keep">
            <keeps></keeps>
        </div> -->
    </div>
</template>

<script>
    import Navbar from './Navbar'
    import Keeps from './Keep'
    export default {
        name: 'Vault',
        data() {
            return {
                newKeep: {},
                formBool: false,
                user: this.$store.state.user
            }
        },
        mounted() {
            this.$store.dispatch('authenticate');
            this.$store.dispatch('getVaultById', { vaultId: this.$route.params.vaultId })
            this.$store.dispatch('getUserKeeps', 
                {   userId: this.user.userId,
                    vaultId: this.$route.params.vaultId
                })
        },
        methods: {
            addKeep(vault) {
                this.$store.dispatch('addKeep',
                    {
                        img: this.newKeep.img,
                        link: this.newKeep.link,
                        description: this.newKeep.description,
                        userId: vault.userId,
                        vaultId: vault.id
                    })
            },
            addToVault(vault, keep, user){
                // console.log("Vault: ", vault)
                // console.log("Keep: ", keep)
                // console.log("User: ", user)
                this.$store.dispatch('addToVault', 
                    {   vaultId: vault.id,
                        keepId: keep.id,
                        userId: user.id
                    })
            }
        },
        computed: {
            vault() {
                return this.$store.state.activeVault
            }
        },
        components: {
            Navbar
        }
    }
</script>

<style scoped>
    form {
        margin-bottom: 2rem;
    }

    .new-form-button {
        margin-bottom: 2rem;
    }
</style>