<template>
    <div>
        <navbar></navbar>
        <h3>Search Keeps</h3>
        <div class="row" style="justify-content: center;">
            <div class="col-sm-9">
                <form @submit.prevent="searchKeeps">
                    <div class="form-group">
                        <input type="query" v-model="searchQuery.query" class="form-control" id="exampleInputEmail1" aria-describedby="emailHelp" placeholder="What do you want to see?">
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
                            <!-- <h5 class="card-title">Card title</h5> -->
                            <p class="card-text">{{keep.description}}</p>
                            <a :href="`${keep.link}`" @click="updateViews(keep)" class="btn btn-primary">go</a>
                            <div class="dropdown">
                                <button class="btn btn-secondary dropdown-toggle" type="button" id="dropdownMenuButton" data-toggle="dropdown" aria-haspopup="true"
                                    aria-expanded="false">
                                    Keep
                                </button>
                                <div class="dropdown-menu" aria-labelledby="dropdownMenuButton">
                                    <div class="dropdown-item" v-for="vault in vaults">
                                        <p @click="addToVault(vault, keep, user)">
                                            {{vault.name}}
                                        </p>
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
            // this.resultsBool = false
        },
        methods: {
            addToVault(vault, keep, user){
                // console.log("Vault: ", vault)
                // console.log("Keep: ", keep)
                // console.log("User: ", user)
                this.$store.dispatch('addToVault', 
                    {   vaultId: vault.id,
                        keepId: keep.id,
                        userId: user.id
                    })
            },
            searchKeeps() {
                this.$store.dispatch('searchKeeps', {query: this.searchQuery.query})
            },
            updateViews(keep) {
                this.$store.dispatch('updateViews', {keep: keep})
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
    .results {
        margin-top: 2rem;
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

    button {
        width: 40%;
        background-color: rgba(87, 46, 60, 0.85);
        font-weight: 700;
        color: ivory;
        margin: 10px;
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