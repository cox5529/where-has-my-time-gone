<script setup lang="ts">
import ProfileImage from './ProfileImage.vue';
import RecorderButtons from './RecorderButtons.vue';

import type { Person } from '@/models/person';
import { usePersonStore } from '@/stores/people';

const props = defineProps<{
  person: Person;
}>();

const people = usePersonStore();

function onDurationPosted(start: string, end: string): void {
  people.addUsage(props.person.id, { id: 0, start, end, type: 'Manual' });
}
</script>

<template>
  <div class="flex flex-col items-center gap-4">
    <ProfileImage :url="props.person.image" :name="props.person.name" />
    <h3 class="text-3xl">{{ props.person.name }}</h3>
    <RecorderButtons @duration-posted="onDurationPosted" />
  </div>
</template>
