import Vue from 'vue'
import Router from 'vue-router'
// @ts-ignore
import Login from '@/components/Login'
// @ts-ignore
import Home from '@/components/Home'
// @ts-ignore
import Navbar from '@/components/Navbar'
// @ts-ignore
import Dashboard from '@/components/Dashboard'
// @ts-ignore
import Vault from '@/components/Vault'

Vue.use(Router)

export default new Router({
  routes: [
    {
      path: '/login',
      name: 'Login',
      component: Login
    },
    {
      path: '/home',
      name: 'Home',
      component: Home
    },
    {
      path: '/dashboard',
      name: 'Dashboard',
      component: Dashboard
    },
    {
      path:'/vault',
      name: 'Vault',
      component: Vault
    }
  ]
})
