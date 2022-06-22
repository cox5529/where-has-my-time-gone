<script setup lang="ts">
import { useAuthenticationStore } from '@/stores/authentication';
import {
  getAuth,
  GoogleAuthProvider,
  signInWithPopup,
  signOut,
} from 'firebase/auth';
import { computed } from 'vue';

const provider = new GoogleAuthProvider();
const auth = getAuth();

const authStore = useAuthenticationStore();

const isAuthenticated = computed(() => !!authStore.user);

async function login(): Promise<void> {
  await signInWithPopup(auth, provider);
}

async function logout(): Promise<void> {
  await signOut(auth);
}
</script>

<template>
  <button
    v-if="!isAuthenticated"
    @click="login"
    class="shadow-md p-2 text-white bg-blue-600 rounded-sm font-semibold flex items-center gap-4 hover:bg-blue-500 active:bg-blue-400"
  >
    <font-awesome-icon icon="fab fa-google"></font-awesome-icon>
    <span>Sign in with Google</span>
  </button>

  <button
    v-else
    @click="logout"
    class="shadow-md p-2 text-white bg-blue-600 rounded-sm font-semibold flex items-center gap-4 hover:bg-blue-500 active:bg-blue-400"
  >
    Logout
  </button>
</template>
