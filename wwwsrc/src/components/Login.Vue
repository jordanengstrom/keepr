<template>
    <div class="login flexor2">
        <div class="flexor">
            <div class="welcome">
                <h1>
                    Welcome to Keepr
                </h1>
            </div>
            <div class="login-holder">
                <div class="icon-section">
                    <i class="far fa-clipboard"></i>
                    <h1>Login</h1>
                </div>
                <div>
                    <div v-if="loginForm == true">
                        <form @submit.prevent="login()">
                            <div class="form-group">
                                <label>Email</label>
                                <input v-model="loginUser.email" class="form-control" type="text" placeholder="Email">
                            </div>
                            <div class="form-group">
                                <label>Password</label>
                                <input v-model="loginUser.password" class="form-control" type="password" placeholder="Password">
                            </div>
                            <button class="btn btn-block btn-outline-success" type="submit">login</button>
                            <!-- <button class="btn btn-block btn-outline-info" @click="loginForm = !loginForm">create account</button> -->
                        </form>
                    </div>
                    <div v-if="loginForm == false">
                        <form @submit.prevent="createSignup()">
                            <div class="form-group">
                                <label>Username</label>
                                <input v-model="signupUser.username" class="form-control" type="text" placeholder="Username" required>
                            </div>
                            <div class="form-group">
                                <label>Email</label>
                                <input v-model="signupUser.email" class="form-control" type="text" placeholder="Email" required>
                            </div>
                            <div class="form-group">
                                <label>Password</label>
                                <input v-model="signupUser.password" class="form-control" type="password" placeholder="Password" required>
                            </div>
                            <button class="btn btn-block btn-outline-success" type="submit">signup</button>
                        </form>
                    </div>
                    <div @click="loginForm = !loginForm">
                        <div v-if="loginForm == false">
                            <button class="btn btn-block btn-outline-info">back to login</button>
                        </div>
                        <div v-if="loginForm == true">
                            <button class="btn btn-block btn-outline-info">create account</button>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</template>

<script>
    export default {
        name: 'Login',
        data() {
            return {
                loginForm: true,
                loginUser: {},
                signupUser: {}
            }
        },
        mounted () {
            this.$store.dispatch('clearActiveKeeps')
        },
        methods: {
            login() {
                // console.log("LOGGING IN: ", this.loginUser)
                this.$store.dispatch('login', this.loginUser)
                this.loginUser = {}
            },
            createSignup() {
                // console.log("SIGNING UP: ", this.signupUser)
                this.$store.dispatch('signup', this.signupUser)
                this.signupUser = {}
            },
        }
    }
</script>


<style scoped>
    a:hover {
        text-decoration: none;
    }

    .login-holder {
        margin-top: 3rem;
    }

    .welcome {
        padding-right: 2rem;
        padding-left: 2rem;
        padding-top: 1rem;
        padding-bottom: 1rem;
        border-radius: 15px;
        background-color: rgba(255, 255, 255, 0.5);
    }

    .btn {
        margin-bottom: 1rem;
    }

    .login {
        background-image: url('../assets/login-min.jpg');
        height: 100vh;
        background-position: center;
        background-repeat: no-repeat;
        background-size: cover;
    }

    .flexor {
        display: flex;
        flex-direction: column;
        justify-content: center;
    }

    .fa-clipboard {
        font-size: 5rem;
    }

    .login-holder {
        padding: 1.5rem;
        background-color: white;
        border: .1rem solid #e2e2e2;
        border-radius: 10px;
        color: #5C4C4D
    }

    .flexor2 {
        display: flex;
        justify-content: center;
    }

    .icon-section {
        margin-bottom: 1.5rem;
        text-align: center;
        font-family: 'Paytone One', sans-serif;
    }

    .btn-outline-success {
        color: #72655C;
        border-color: #72655C;
    }

    .btn-outline-success:hover {
        color: #fff;
        border-color: #72655C;
        background-color: #72655C;
    }

    .btn-outline-info {
        border-color: #7798B9;
        color: #7798B9;
    }

    .btn-outline-info:hover {
        color: #fff;
        border-color: #7798B9;
        background-color: #7798B9;
    }
    
</style>