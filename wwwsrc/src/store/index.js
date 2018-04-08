import vue from 'vue'
import vuex from 'vuex'
import axios from 'axios'
import router from '../router/index'


// var baseUrl = production ? '//port-vue-kan-ban.herokuapp.com/' : '//localhost:3000/'
var baseUrl = '//localhost:5000/'
var auth = axios.create({
    baseURL: baseUrl + 'account/',
    withCredentials: true
});

vue.use(vuex);

export default new vuex.Store({
    state: {
        user: {}
    },
    mutations: {
        loginUser(state, payload) {
            state.user = payload
        },
        clearData(state, payload){
            state.user = {}
        }
    },
    actions: {
        //region START AUTH ROUTES
        login({ commit, dispatch }, payload) {
            auth.post('login', payload)
                .then(res => {
                    console.log("LOGGED IN USER: ", res.config.data)
                    commit('loginUser', res.data)
                    router.push({ name: 'Home' })
                })
                .catch(err => {
                    console.log(err);
                    console.log('INVALID USERNAME OR PASSWORD')
                })
        },
        authenticate({ commit, dispatch }) {
                auth.get('authenticate')
                    .then(res => {
                        console.log(res)
                        commit('loginUser', res.config.data)
                    })
                    .catch(err => {
                        console.log(err)
                        router.push({ name: 'Login' })
                    })
        },
        signup({ commit, dispatch }, payload) {
            console.log("SIGNING UP PAYLOAD: ", payload)
            auth.post('register', payload)
                .then(res => {
                    console.log("SIGNED UP USER: ", res.config.data)
                    commit('loginUser', res.config.data)
                    router.push({ name: 'Home' })
                })
                .catch(err => {
                    console.log(err)
                })
        },
        logout({ commit, dispatch }) {
            auth.delete('logout')
                .then(res => {
                    console.log(res);
                    commit('loginUser', {})
                    commit('clearData')
                    router.push({name: 'Login'})
                })
        }
        //endregion END AUTH ACTIONS
    }
});