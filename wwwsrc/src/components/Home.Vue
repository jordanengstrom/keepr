<template>
  <div class="home">
      <div class="row" style="justify-content: center;">
          <h1>Home</h1>
      </div>
      <div>
          <button type="button" class="btn btn-secondary" style="margin-top: 5rem;" @click="logout()">Logout</button>
      </div>
  </div>
</template>

<script>
  export default {
      name: 'Home',
      data() {
          return {
          }
      },
    //   mounted() {
    //       this.$store.dispatch('authenticate')
    //   },
      methods: {
          logout() {
              this.$store.dispatch('logout')
          }
      }
  }
</script>

<style>
</style>