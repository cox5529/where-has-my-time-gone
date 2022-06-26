<script setup lang="ts">
import AddPersonButton from '../components/AddPersonButton.vue';
import PersonRecorder from '../components/PersonRecorder.vue';

import { usePersonStore } from '@/stores/people';

const people = usePersonStore();
await people.load();
</script>

<template>
  <main class="flex gap-16 items-start flex-wrap">
    <PersonRecorder
      v-for="(person, index) in people.people"
      :key="index"
      :person="person"
    />

    <AddPersonButton />
  </main>
</template>
