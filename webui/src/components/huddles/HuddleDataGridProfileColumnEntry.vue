<script setup lang="ts">
import ProfileImage from '../ProfileImage.vue';

import type { FetchHuddlesProfileResponse } from '@/models/responses/fetch-huddles-response';

export type Huddle = {
  start: string;
  end: string;
  left: number;
  width: number;
};

defineProps<{
  profile: FetchHuddlesProfileResponse;
}>();
</script>

<template>
  <div
    class="text-center h-36 p-2 flex items-center flex-col text-sm border-dashed border-b justify-center gap-2 relative"
  >
    <ProfileImage
      class="w-20"
      :url="profile.profileImage"
      :name="profile.name"
    />
    <span class="overflow-hidden"> {{ profile.name }} </span>
  </div>
</template>
