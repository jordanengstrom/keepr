<template>
    <div>
        <navbar></navbar>
        <h3>Search Keeps</h3>
        <div class="row" style="justify-content: center;">
            <div class="col-sm-9">
                <form @submit.prevent="searchKeeps">
                    <div class="form-group">
                        <input type="query" v-model="searchQuery.query" class="form-control" id="exampleInputEmail1" aria-describedby="emailHelp"
                            placeholder="What do you want to see?">
                    </div>
                    <button type="submit" class="btn">Submit</button>
                </form>
            </div>
        </div>
        <div v-if="resultsBool == true">
            <div v-for="result in results">
                <result></result>
            </div>
        </div>
        <div v-else>
            <div class="row keep-row">
                <div v-for="keep in keeps" :keep="keep">
                    <div class="card">
                        <img class="card-img-top" :src="`${keep.img}`" alt="Card image cap">
                        <div class="card-body">
                            <div>
                                <p class="card-text">{{keep.description}}</p>
                            </div>
                            <div class="col-align">
                                <div>
                                    <p class="card-text">Views: {{keep.views}}</p>
                                </div>
                                <div>
                                    <p class="card-text">Keeps: {{keep.keeps}}</p>
                                </div>
                            </div>
                            <div class="col-align">
                                <div>
                                    <a :href="`${keep.link}`" @click="updateViews(keep)" class="btn btn-eye">
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
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</template>

<script>
    import Keep from './Keep'
    import Result from './Result'
    import Navbar from './Navbar'
    export default {
        name: 'Home',
        data() {
            return {
                userEmail: {
                    email: ''
                },
                searchQuery: {
                    query: ''
                },
                resultsBool: false
            }
        },
        mounted() {
            this.$store.dispatch('authenticate')
            this.$store.dispatch('getAllKeeps')
        },
        methods: {
            addToVault(vault, keep, user) {
                this.$store.dispatch('addToVault',
                    {
                        vaultId: vault.id,
                        keepId: keep.id,
                        userId: user.id
                    })
                this.keepCount(keep, vault)
            },
            searchKeeps() {
                this.$store.dispatch('searchKeeps', { query: this.searchQuery.query })
            },
            updateViews(keep) {
                this.$store.dispatch('updateViews', keep)
            },
            keepCount(keep, vault) {
                this.$store.dispatch('keepCount', 
                {
                    keep: keep,
                    vaultId: vault.id
                })
            }
        },
        components: {
            Navbar,
            Result
        },
        computed: {
            user() {
                return this.$store.state.user
            },
            keeps() {
                return this.$store.state.keeps
            },
            vaults() {
                return this.$store.state.vaults
            }
        }
    }

</script>

<style scoped>
    .btn-eye {
        background-color: #5D7638;
        outline-color: #5D7638;
        color: #ffffff;
    }

    .results {
        margin-top: 2rem;
    }

    .col-align {
        display: flex;
        flex-direction: row;
        justify-content: space-around;
        align-items: center;
    }

    #dropdownMenuButton {
        width: 3rem;
    }

    button {
        background-color: rgba(87, 46, 60, 0.85);
        font-weight: 700;
        color: ivory;
        margin: 10px;
    }

    .keep-row {
        margin-left: 1rem;
        justify-content: center;
    }

    h3 {
        margin-top: 1rem;
        margin-bottom: 1rem;
    }

    .user-results {
        background-color: rgba(255, 255, 255, 0);
        margin: 10px;
        border: solid 3px rgba(87, 46, 60, 0.726);
        border-radius: 10px;
        width: 25%;
    }

    .flex {
        display: flex;
        flex-direction: column;
        align-items: center;
        justify-content: center;
    }

    .card {
        width: 20rem;
        height: auto;
        margin-left: 1rem;
        margin-right: 1rem;
        margin-top: 1rem;
    }

    /* .card-img-top {
        height: 75px;
        width: 75px;
    } */
</style>