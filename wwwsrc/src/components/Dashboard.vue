<template>
    <div>
        <navbar></navbar>
        <h2>Welcome to your dashboard</h2>
        <button @click="formBool = !formBool" class="btn new-form-button">Add a vault</button>
        <div v-if="formBool == true">
            <div class="row form-row">
                <form @submit.prevent="addVault(user)">
                    <div class="form-group">
                        <label for="vault-name">Vault Name</label>
                        <input v-model="newVault.name" type="text" class="form-control" id="vault-name" aria-describedby="form name" placeholder="Name your vault">
                    </div>
                    <div class="form-group">
                        <label for="vault-description">Vault Description</label>
                        <input v-model="newVault.description" type="text" class="form-control" id="vault-description" placeholder="What goes in this vault?">
                    </div>
                    <button type="submit" class="btn green-btn">Submit</button>
                </form>
            </div>
        </div>
        <div class="list-group" v-for="vault in vaults">
            <div class="row list-group-row">
                <div class="col-sm-9">
                    <a href="#" class="list-group-item list-group-item-action flex-column align-items-start">
                        <div class="d-flex w-100 justify-content-between">
                            <router-link :to="{path: '/vault/' + vault.id }">
                                <div class="name-click">
                                    <h5 class="mb-1">{{vault.name}}</h5>
                                </div>
                            </router-link>
                            <div v-if="updatedFormBool == true  && vaultId == vault.id">
                                <form @submit.prevent="updateVault(user, vault)">
                                    <div class="form-group">
                                        <label for="vault-name">Update vault Name</label>
                                        <input v-model="updatedVault.name" type="text" class="form-control" id="vault-name" aria-describedby="form name" placeholder="Name your vault">
                                    </div>
                                    <div class="form-group">
                                        <label for="vault-description">Update vault Description</label>
                                        <input v-model="updatedVault.description" type="text" class="form-control" id="vault-description" placeholder="What goes in this vault?">
                                    </div>
                                    <button type="submit" class="btn green-btn">Submit</button>
                                </form>
                            </div>
                            <div class="ellipsis" data-toggle="dropdown">
                                <i class="fas fa-ellipsis-v"></i>
                            </div>
                            <div class="dropdown-menu">
                                <div class="dropdown-item">
                                    <p @click="deleteVault(user, vault)">delete</p>
                                </div>
                                <div class="dropdown-item">
                                    <p @click="updatedFormBool = !updatedFormBool, vaultId = vault.id">update</p>
                                </div>
                            </div>
                        </div>
                        <p class="mb-1"></p><em>{{vault.description}}</em></p>
                    </a>
                </div>
            </div>
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
                updatedFormBool: false,
                newVault: {},
                updatedVault: {},
                vaultId: '',
            }
        },
        mounted() {
            // this.$store.dispatch('clearActiveKeeps')
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
                this.formBool = false;
            },
            deleteVault(user, vault) {
                this.$store.dispatch('deleteVault',
                    {
                        vaultId: vault.id,
                        userId: user.id
                    })
            },
            updateVault(user, vault) {
                this.$store.dispatch('updateVault',
                    {
                        id: vault.id,
                        name: this.updatedVault.name,
                        description: this.updatedVault.description,
                        userId: user.id
                    })
                this.updatedFormBool = false;
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
    h5 {
        color: rgba(87, 46, 60, 0.85);
        font-weight: 700;
    }

    .green-btn {
        background-color: #5D7638;
        outline-color: #5D7638;
        color: #ffffff;
    }

    .name-click {
        padding: 0.5rem;
        cursor: pointer;
    }

    .list-group-item {
        margin-bottom: 2rem;
    }

    .list-group-row {
        justify-content: center;
        margin-top: 1.5rem;
    }

    .ellipsis {
        padding: 0.5rem;
        cursor: pointer;
    }

    a:hover {
        text-decoration: none;
        cursor: pointer;
    }

    a:-webkit-any-link {
        cursor: default !important;
    }

    h2 {
        margin-top: 1rem;
        margin-bottom: 1rem;
    }

    form {
        margin-bottom: 2rem;
    }

    .form-row {
        justify-content: center;
    }

    .new-form-button {
        margin-bottom: 4rem;
        width: 40%;
        background-color: rgba(87, 46, 60, 0.85);
        font-weight: 700;
        color: ivory;
        margin: 10px;
    }

    /* come back to this */

    .submit-btn {
        margin-bottom: 2rem;
        width: 40%;
        background-color: rgba(87, 46, 60, 0.85);
        font-weight: 700;
        color: ivory;
        margin: 10px;
    }
</style>