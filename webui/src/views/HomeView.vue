<script setup lang="ts">
import PersonRecorder from '../components/PersonRecorder.vue';
import AddPersonButton from '../components/buttons/AddPersonButton.vue';

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
