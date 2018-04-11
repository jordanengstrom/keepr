import vue from 'vue'
import vuex from 'vuex'
import axios from 'axios'
import router from '../router/index'


// var baseUrl = production ? '//port-vue-kan-ban.herokuapp.com/' : '//localhost:3000/'
var baseUrl = 'http://localhost:5000/'
var auth = axios.create({
    baseURL: baseUrl + 'account/',
    withCredentials: true
    // headers: {
    //     accept: 'application/json',
    //     'accept-language': 'en_US',
    //     'Content-Type': 'application/x-www-form-urlencoded'
    // }
});

var api = axios.create({
    baseURL: baseUrl + 'api/',
    withCredentials: true
});

vue.use(vuex);

export default new vuex.Store({
    state: {
        user: {},
        vaultkeeps: [],
        vaults: [],
        keeps: [],
        results: [],
        activeVault: {}
    },
    mutations: {
        setVaultKeeps(state, payload) {
            state.vaultkeeps = payload
        },
        setKeeps(state, payload) {
            state.keeps = payload
        },
        setVaults(state, payload) {
            state.vaults = payload
        },
        setActiveVault(state, payload) {
            state.activeVault = payload
        },
        loginUser(state, payload) {
            state.user = payload
        },
        clearData(state, payload) {
            state.user = {}
        }
    },
    actions: {
        //region KEEPS
        getKeepById({ commit, dispatch }, payload) {
            console.log("GETKEEPBYID: ", payload)
            api.get('/keeps/' + payload.keepId)
                .then(res => {
                    console.log(res)
                })
        },
        getAllKeeps({ commit, dispatch }, payload) {
            api.get('keeps')
                .then(res => {
                    var out = []
                    for (var i = 0; i < res.data.length; i++) {
                        var keepElem = res.data[i]
                        if(keepElem.public){
                            out.push(keepElem)
                        }
                    }
                    // console.log("KEEPS RES.DATA: ", res.data)
                    commit('setKeeps', out)
                })
        },
        addKeep({ commit, dispatch }, payload) {
            console.log("INCOMING KEEP PAYLOAD: ", payload)
            api.post('keeps', payload)
                .then(res => {
                    console.log("RES.DATA", res.data)
                    var dispatchedPayload =
                        {
                            vaultId: payload.vaultId,
                            keepId: res.data.id,
                            userId: payload.userId,
                        }
                    dispatch('addToVault', dispatchedPayload);
                })
        },
        addToVault({ commit, dispatch }, payload) {
            console.log("addToVault Payload: ", payload)
            api.post('vaultkeeps', payload)
                .then(res => {
                    console.log("RES.DATA: ", res.data)
                })
        },
        getVaultKeeps({commit, dispatch}, payload) {
            api.get('keeps/report/' + payload.vaultId)
                .then(res => {
                    console.log("VAULT KEEPS: ", res.data);
                    commit('setVaultKeeps', res.data)
                })
        },
        getUserKeeps({ commit, dispatch }, payload) {
            api.get('keeps/report/' + payload.userId)
                .then(res => {
                    // console.log("RES.DATA: ", res.data)
                    commit('setKeeps', res.data)
                })
        },
        searchKeeps({ commit, dispatch }, payload) {
            var searchTerm = payload.query.toLowerCase();
            api.get('keeps')
                .then(res => {
                    var arr = res.data
                    var out = []
                    for (var i = 0; i < arr.length; i++) {
                        var keepElem = arr[i]
                        if (keepElem.description.toLowerCase().includes(searchTerm)) {
                            out.push(keepElem)
                        }
                    }
                    // console.log("OUT: ", out);
                    commit('setKeeps', out);
                })
        },
        updateViews({ commit, dispatch }, payload) {
            api.get('keeps/' + payload.keep.id)
                .then(res => {
                    console.log("RES.DATA: ", res.data)
                    // commit('setKeeps', res.data)
                })
        },

        //region VAULTS
        addVault({ commit, dispatch }, payload) {
            api.post('vaults', payload)
                .then(res => {
                    // console.log("RES.DATA: ", res.data)
                    dispatch('getUserVaults', res.data);
                })
                .catch(err => {
                    console.log(err);
                })
        },
        getUserVaults({ commit, dispatch }, payload) {
            api.get('vaults/report/' + payload.userId)
                .then(res => {
                    // console.log("USER VAULTS: ", res.data)
                    commit('setVaults', res.data)
                })
        },
        getVaultById({ commit, dispatch }, payload) {
            api.get('vaults/' + payload.vaultId)
                .then(res => {
                    // console.log(res);
                    commit('setActiveVault', res.data)
                })
        },
        deleteVault({ commit, dispatch }, payload) {
            api.delete('vaults/' + payload.vaultId)
                .then(res => {
                    dispatch('getUserVaults', payload)
                })
        },
        updateVault({ commit, dispatch }, payload) {
            console.log("UPDATED VAULT PAYLOAD: ", payload)
            api.put('vaults/' + payload.id, payload)
                .then(res => {
                    console.log("RES: ", res)
                    dispatch('getUserVaults', payload);
                })
        },
        // endregion VAULTS

        //region START AUTH ROUTES
        login({ commit, dispatch }, payload) {
            // console.log("Attempting to log in user: ", payload)
            auth.post('login', payload)
                .then(res => {
                    // console.log("LOGGED IN USER: ", res.config.data)
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
                    // console.log("AUTH RES.DATA", res.data)
                    commit('loginUser', res.data)
                    res.data["userId"] = res.data.id
                    var dispatchedPayload = res.data
                    dispatch('getUserVaults', dispatchedPayload)
                })
                .catch(err => {
                    console.log(err)
                    router.push({ name: 'Login' })
                })
        },
        signup({ commit, dispatch }, payload) {
            // console.log("SIGNING UP PAYLOAD: ", payload)
            auth.post('register', payload)
                .then(res => {
                    // console.log("SIGNED UP USER: ", res.config.data)
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
                    router.push({ name: 'Login' })
                })
        }
        //endregion END AUTH ACTIONS
    }
});