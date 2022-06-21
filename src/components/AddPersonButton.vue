<script setup lang="ts">
import { usePersonStore } from '@/stores/people';

const people = usePersonStore();

function insertData(): void {
  people.addPerson('Joe');
}
</script>

<template>
  <button @click="insertData">
    <font-awesome-icon class="w-40 h-40" icon="fa-solid fa-circle-plus" />
  </button>
</template>
