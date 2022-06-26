<script setup lang="ts">
import GoogleSignInButton from './GoogleSignInButton.vue';
import NavigationLink from './NavigationLink.vue';
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
      <NavigationLink to="/">Home</NavigationLink>
      <NavigationLink to="/analytics">Analytics</NavigationLink>
      <NavigationLink v-if="loggedIn" to="/huddles">Huddles</NavigationLink>
    </div>
    <div class="flex gap-8 items-center">
      <p v-if="email">Hello, {{ email }}</p>
      <GoogleSignInButton />
    </div>
  </div>
</template>
