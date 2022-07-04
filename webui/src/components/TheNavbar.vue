<script setup lang="ts">
import AppNavigationLink from './buttons/AppNavigationLink.vue';
import GoogleSignInButton from './buttons/GoogleSignInButton.vue';
import { computed } from 'vue';

import { useAuthenticationStore } from '../stores/authentication';

const authStore = useAuthenticationStore();

const email = computed(() => authStore.user?.email);
const loggedIn = computed(() => !!authStore.isLoggedIn);
</script>

<template>
  <div
    class="h-16 bg-slate-700 flex items-center text-white gap-8 justify-between px-4"
  >
    <div class="flex gap-4 items-center">
      <AppNavigationLink to="/">Home</AppNavigationLink>
      <AppNavigationLink to="/analytics">Analytics</AppNavigationLink>
      <AppNavigationLink v-if="loggedIn" to="/huddles"
        >Huddles</AppNavigationLink
      >
    </div>
    <div class="flex gap-8 items-center">
      <p v-if="email">Hello, {{ email }}</p>
      <GoogleSignInButton />
    </div>
  </div>
</template>
