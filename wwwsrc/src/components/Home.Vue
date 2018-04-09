<template>
        <div>
            <navbar></navbar>
            <h3>Search Keeps</h3>
            <div class="row" style="justify-content: center;">
                <div class="col-sm-9">
                    <form @submit.prevent="searchByEmail">
                        <div class="form-group">
                            <input type="email" v-model="userEmail.email" class="form-control" id="exampleInputEmail1" aria-describedby="emailHelp" placeholder="What do you want to see?">
                        </div>
                        <button type="submit" class="btn">Submit</button>
                    </form>
                </div>
            </div>
        </div>
    </template>
    
    <script>
        import Navbar from './Navbar'
        export default {
            name: 'Home',
            data() {
                return {
                    userEmail: {
                        email: ''
                    }
                }
            },
            mounted() {
                this.$store.dispatch('authenticate');
            },
            methods: {
                searchByEmail() {
                    this.$store.dispatch('searchByEmail', this.userEmail);
                },
                followUser(user, userToAdd) {
                    this.$store.dispatch('followUser', { user: user, userToAdd: userToAdd });
                }
            },
            components: {
                Navbar
            },
            computed: {
                foundUser() {
                    return this.$store.state.foundUser;
                },
                user() {
                    return this.$store.state.user;
                }
            }
        }
    
    </script>
    
    <style scoped>
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
    </style>