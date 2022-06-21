<script setup lang="ts">
import { useModalStore } from '@/stores/modals';

const modals = useModalStore();

function show(): void {
  modals.addPerson = true;
}
</script>

<template>
  <button @click="show">
    <font-awesome-icon class="w-40 h-40" icon="fa-solid fa-circle-plus" />
  </button>
</template>
