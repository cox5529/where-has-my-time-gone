<script setup lang="ts">
import HuddleDataGridRow from './HuddleDataGridRow.vue';

import type { FetchHuddlesProfileResponse } from '@/models/responses/fetch-huddles-response';

defineProps<{
  profiles: FetchHuddlesProfileResponse[];
  timeSlices: number;
  date: string;
}>();
</script>

<template>
  <div class="absolute top-24 left-32">
    <HuddleDataGridRow
      v-for="profile in profiles"
      :profile="profile"
      :key="profile.userProfileId"
      :date="date"
      :time-slices="timeSlices"
    />
  </div>
</template>
