<template>
    <div>
        <navbar></navbar>
        <h1>{{vault.name}}</h1>
        <h3>{{vault.description}}</h3>
        <button @click="formBool = !formBool" class="btn new-form-button">New Keep</button>
        <div v-if="formBool == true">
            <div class="row form-row">
                <form @submit.prevent="addKeep(vault)">
                    <div class="form-group">
                        <label for="keep-name">Image Link</label>
                        <input v-model="newKeep.img" type="text" class="form-control" id="keep-img" aria-describedby="form name" placeholder="Link to image of content">
                    </div>
                    <div class="form-group">
                        <label for="keep-link">Link to content</label>
                        <input v-model="newKeep.link" type="text" class="form-control" id="keep-description" placeholder="Link to content">
                    </div>
                    <div class="form-group">
                        <label for="keep-description">Keep Description</label>
                        <br>
                        <!-- <input v-model="newKeep.description" type="text" class="form-control" id="keep-description" placeholder="Blog about the keep"> -->
                        <textarea v-model="newKeep.description" maxlength="235" placeholder="Blog about the keep" type="text" class="form-control"
                            id="keep-description" name="message" rows="8" cols="30"></textarea>
                    </div>
                    <div class="bottom-margin">
                        <input type="radio" v-model="newKeep.public" name="boolVal" value="true" checked> Public
                        <input type="radio" v-model="newKeep.public" name="boolVal" value="false" checked> Private
                    </div>
                    <button type="submit" class="btn green-btn">Submit</button>
                </form>
            </div>
        </div>
        <div class="row keep-row">
            <div v-for="activeKeep in activeKeeps" :activeKeep="activeKeep">
                <div class="card">
                    <img class="card-img-top" :src="`${activeKeep.img}`" alt="Card image cap">
                    <div class="card-body">
                        <div>
                            <p class="card-text">{{activeKeep.description}}</p>
                        </div>
                        <div class="col-align">
                            <div>
                                <p class="card-text">Views: {{activeKeep.views}}</p>
                            </div>
                            <div>
                                <p class="card-text">Keeps: {{activeKeep.views}}</p>
                            </div>
                        </div>
                        <div class="col-align">
                            <div>
                                <a :href="`${activeKeep.link}`" @click="updateViews(activeKeep)" class="btn btn-eye">
                                    <i class="fas fa-eye"></i>
                                </a>
                            </div>
                            <div>
                                <div class="dropdown">
                                    <button class="btn btn-secondary dropdown-toggle" type="button" id="dropdownMenuButton" data-toggle="dropdown" aria-haspopup="true"
                                        aria-expanded="false">
                                        K
                                    </button>
                                    <div class="dropdown-menu" aria-labelledby="dropdownMenuButton">
                                        <div class="dropdown-item" v-for="vault in vaults">
                                            <p @click="addToVault(vault, keep, user)">
                                                {{vault.name}}
                                            </p>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <!-- stretch goal:
                             <a href="#" class="btn btn-primary">share</a> -->
                        <div>
                            <i class="fas fa-ellipsis-h"></i>
                        </div>
                    </div>
                </div>
            </div>
        </div>
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
            this.$store.dispatch('clearActiveKeeps')
            this.$store.dispatch('authenticate')
            this.$store.dispatch('getVaultById', { vaultId: this.$route.params.vaultId })
            // this.$store.dispatch('getUserKeeps',
            //     {
            //         userId: this.user.userId,
            //         vaultId: this.$route.params.vaultId
            //     })
            this.$store.dispatch('getVaultKeeps', { vaultId: this.$route.params.vaultId })
            // this.$store.dispatch('getActiveKeeps', { vaultkeeps: this.$store.state.vaultkeeps })
        },
        methods: {
            addKeep(vault) {
                this.$store.dispatch('addKeep',
                    {
                        img: this.newKeep.img,
                        link: this.newKeep.link,
                        description: this.newKeep.description,
                        userId: vault.userId,
                        vaultId: vault.id,
                        public: this.newKeep.public
                    })
            },
            updateViews(keep) {
                console.log(keep)
                this.$store.dispatch('updateViews', { keep: keep })
            }
        },
        computed: {
            vault() {
                return this.$store.state.activeVault
            },
            activeKeeps() {
                // console.log(activeKeeps)
                return this.$store.state.activeKeeps
            },
            vaults() {
                return this.$store.state.vaults
            }
        },
        components: {
            Navbar
        }
    }
</script>

<style scoped>
    #dropdownMenuButton {
        width: 3rem;
    }

    .btn-eye {
        background-color: #5D7638;
        outline-color: #5D7638;
        color: #ffffff;
    }

    button {
        width: 40%;
        background-color: rgba(87, 46, 60, 0.85);
        font-weight: 700;
        color: ivory;
        margin: 10px;
    }

    .green-btn {
        background-color: #5D7638;
        outline-color: #5D7638;
        color: #ffffff;
    }

    .bottom-margin {
        margin-bottom: 0.5rem;
    }

    .col-align {
        display: flex;
        flex-direction: row;
        justify-content: space-around;
        align-items: center;
    }

    .keep-row {
        margin-left: 1rem;
        justify-content: center;
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

    .card {
        width: 20rem;
        height: auto;
        margin-left: 1rem;
        margin-right: 1rem;
        margin-top: 1rem;
    }
</style>